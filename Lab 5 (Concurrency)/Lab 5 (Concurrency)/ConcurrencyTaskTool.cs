using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lab_5__Concurrency_
{
    internal class ConcurrencyTaskTool
    {
    }
}

        // 50 объектов Bike
        private List<Bike> bikes50;

        // Имена файлов для распределения 50 записей (по 10 в каждом)
        private readonly string[] fileNames = new string[]
        {
            "file1.xml",
            "file2.xml",
            "file3.xml",
            "file4.xml",
            "file5.xml"
        };

        // Итоговый объединённый файл
        private const string mergedFile = "mergedFile.xml";

        // Потокобезопасный словарь, где ключ – имя файла,
        // а значение – ConcurrentQueue с записями из этого файла.
        private ConcurrentDictionary<string, ConcurrentQueue<Bike>> fileDictionary =
            new ConcurrentDictionary<string, ConcurrentQueue<Bike>>();

        // Для ProgressBar
        private int recordsRead = 0;
        private int totalRecords = 0; // Всего 50 записей

        // Токен отмены для фонового сортировщика
        private CancellationTokenSource sortCts = new CancellationTokenSource();

        public ConcurrencyHandler()
        {
            bikes50 = Generate50Bikes();
            totalRecords = bikes50.Count;
        }

        /// <summary>
        /// Задание 0:
        /// Генерирует 50 экземпляров Bike и распределяет их по 5 файлам (по 10 записей в каждом).
        /// </summary>
        public async Task GenerateFilesAsync()
        {
            // Разбиваем 50 объектов на группы по 10
            var groups = bikes50
                .Select((bike, index) => new { bike, index })
                .GroupBy(x => x.index / 10)
                .Select(g => g.Select(x => x.bike).ToList())
                .ToList();

            // Параллельная запись каждой группы в свой файл
            var tasks = groups.Select((group, idx) =>
                Task.Run(() => SerializeBikeGroup(group, fileNames[idx])))
                .ToArray();

            await Task.WhenAll(tasks);
            Console.WriteLine("50 записей распределены по 5 файлам.");
        }

        /// <summary>
        /// Сериализует список объектов Bike в указанный файл.
        /// </summary>
        private void SerializeBikeGroup(List<Bike> bikesToSave, string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Bike>));
            using (var writer = new StreamWriter(fileName))
            {
                serializer.Serialize(writer, bikesToSave);
            }
        }

        /// <summary>
        /// Генерирует 50 объектов Bike.
        /// </summary>
        private List<Bike> Generate50Bikes()
        {
            const string manufacturerName = "Bike Co.";
            const string manufacturerAddress = "123 Street";
            Manufacturer manufacturer = Manufacturer.Create(manufacturerName, manufacturerAddress, false);

            List<Bike> bikes = new List<Bike>();
            for (int i = 1; i <= 50; i++)
            {
                bikes.Add(Bike.Create(i, $"Bike_{i}", $"SN{i:0000}", "Mountain", manufacturer));
            }
            return bikes;
        }

        /// <summary>
        /// Задание 1:
        /// Читает данные из 5 файлов параллельно (по одному файлу в задаче),
        /// заполняет потокобезопасный словарь, обновляет ProgressBar и объединяет записи в один итоговый файл.
        /// Затем выводит содержимое словаря в консоль.
        /// </summary>
        public async Task ReadFilesConcurrentlyAsync()
        {
            fileDictionary.Clear();
            recordsRead = 0;

            var tasks = fileNames.Select(file => Task.Run(() => ReadFileAndPopulateDictionaryAsync(file)))
                                  .ToArray();
            await Task.WhenAll(tasks);

            MergeDictionaryToFile();
            Console.WriteLine("\nОбъединённый файл создан.");

            foreach (var kvp in fileDictionary)
            {
                Console.WriteLine($"\nСодержимое файла [{kvp.Key}]:");
                foreach (var bike in kvp.Value)
                {
                    Console.WriteLine($"Bike ID: {bike.Id}, Model: {bike.Model}");
                }
            }
        }

        /// <summary>
        /// Читает файл, десериализует список Bike, добавляет каждую запись в ConcurrentQueue,
        /// обновляет ProgressBar и выполняет задержку 100 мс после каждой записи.
        /// </summary>
        private async Task ReadFileAndPopulateDictionaryAsync(string fileName)
        {
            List<Bike> bikesFromFile;
            XmlSerializer serializer = new XmlSerializer(typeof(List<Bike>));
            using (var reader = new StreamReader(fileName))
            {
                bikesFromFile = (List<Bike>)serializer.Deserialize(reader);
            }

            var queue = new ConcurrentQueue<Bike>();
            foreach (var bike in bikesFromFile)
            {
                queue.Enqueue(bike);
                Interlocked.Increment(ref recordsRead);
                UpdateProgressBar();
                await Task.Delay(100); // задержка для наглядности ProgressBar
            }
            fileDictionary.TryAdd(fileName, queue);
        }

        /// <summary>
        /// Обновляет ProgressBar в консоли на основании количества прочитанных записей.
        /// </summary>
        private void UpdateProgressBar()
        {
            double progress = (double)recordsRead / totalRecords;
            int progressWidth = 30;
            int filledBars = (int)(progress * progressWidth);
            string bar = new string('#', filledBars) + new string('-', progressWidth - filledBars);
            Console.CursorLeft = 0;
            Console.Write($"Progress: [{bar}] {progress * 100:0.0}%");
        }

        /// <summary>
        /// Объединяет записи из всех потокобезопасных коллекций словаря и записывает результат в итоговый файл.
        /// </summary>
        private void MergeDictionaryToFile()
        {
            List<Bike> mergedBikes = fileDictionary.Values.SelectMany(q => q).ToList();
            XmlSerializer serializer = new XmlSerializer(typeof(List<Bike>));
            using (var writer = new StreamWriter(mergedFile, false))
            {
                serializer.Serialize(writer, mergedBikes);
            }
        }

        /// <summary>
        /// Задание 2:
        /// Запускает фонового обработчик, который каждую секунду сортирует значения словаря
        /// для каждого ключа по алфавитному порядку свойства Id и выводит результат в консоль.
        /// </summary>
        public void StartSortingHandler()
        {
            Task.Run(async () =>
            {
                while (!sortCts.Token.IsCancellationRequested)
                {
                    foreach (var key in fileDictionary.Keys)
                    {
                        if (fileDictionary.TryGetValue(key, out ConcurrentQueue<Bike> queue))
                        {
                            // Получаем snapshot и выполняем сортировку по свойству Id
                            var sorted = queue.ToList().OrderBy(b => b.Id.ToString()).ToList();
                            Console.WriteLine($"\n[СОРТИРОВКА] Файл {key}:");
                            foreach (var bike in sorted)
                            {
                                Console.Write($"[{bike.Id}] ");
                            }
                            Console.WriteLine();
                        }
                    }
                    await Task.Delay(1000); // обновление раз в секунду
                }
            }, sortCts.Token);
        }

        /// <summary>
        /// Останавливает фоновый обработчик сортировки.
        /// </summary>
        public void StopSortingHandler()
        {
            if (!sortCts.IsCancellationRequested)
            {
                sortCts.Cancel();
            }
        }
    }
}