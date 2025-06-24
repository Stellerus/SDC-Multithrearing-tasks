using BikeLibrary;
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
        // List of bike
        private List<Bike> allBikes;
        // 50 for this task
        private const int numberOfBikes = 50;

        // Names of files that this class spreads bikes across
        private readonly string[] fileNames = new string[]
        {
            "bikesSerializedPart1.xml",
            "bikesSerializedPart2.xml",
            "bikesSerializedPart3.xml",
            "bikesSerializedPart4.xml",
            "bikesSerializedPart5.xml"
        };

        // Single file with all bikes
        private const string allBikesSerialized = "allBikesSerialized.xml";

        // Concurrent Dictionary with file name as key and
        // value as ConcurrentQueue with records from the file
        private ConcurrentDictionary<string, ConcurrentQueue<Bike>> fileDictionary = new();

        // Stats for progress bar realization
        private int recordsRead = 0;
        private int totalRecords = 0; // 50 total

        // Token to stop sorter
        private CancellationTokenSource sortCts = new();

        public ConcurrencyTaskTool()
        {
            allBikes = GenerateBikes(numberOfBikes);
            totalRecords = allBikes.Count;
        }

        /// <summary>
        /// Generates 50 instances of Bike and spreads across 5 files (10 per file)
        /// </summary>
        public async Task GenerateFilesAsync()
        {
            // Spread 50 instances in groups of 10
            var groups = allBikes
                .Select((bike, index) => new { bike, index })
                .GroupBy(x => x.index / 10)
                .Select(g => g.Select(x => x.bike).ToList())
                .ToList();

            // Parallel record into array
            var tasks = groups.Select((group, idx) =>
                Task.Run(() => SerializeBikeGroup(group, fileNames[idx])))
                .ToArray();

            await Task.WhenAll(tasks);
            Console.WriteLine("50 instances are spread across 5 files");
        }


        /// <summary>
        /// Serializes list of bikes into a file
        /// </summary>
        /// <param name="bikesToSave"> List with bikes to Serialize </param>
        /// <param name="fileName"> name of destination file </param>
        private void SerializeBikeGroup(List<Bike> bikesToSave, string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Bike>));
            using (var writer = new StreamWriter(fileName))
            {
                serializer.Serialize(writer, bikesToSave);
            }
        }

        /// <summary>
        /// Generates 50 instances of Bike.
        /// </summary>
        /// /// <param name="numberOfBikes"> How many bikes to generate</param>
        private List<Bike> GenerateBikes(int numberOfBikes)
        {
            const string manufacturerName = "Bike Co.";
            const string manufacturerAddress = "123 Street";

            // Bike naming constants
            const string BikeNamePrefix = "Bike_";
            const string BikeSerialNumberPrefix = "SN";
            const string BikeSerialNumberPostfix = ":0000";
            const string BikeType = "Mountain";


            Manufacturer manufacturer = 
                Manufacturer.Create(manufacturerName, manufacturerAddress, false);

            List<Bike> bikes = new List<Bike>();
            for (int i = 1; i <= numberOfBikes; i++)
            {
                bikes.Add
                    (
                        Bike.Create
                        (
                            i, 
                            $"{BikeNamePrefix}{i}", 
                            $"{BikeSerialNumberPrefix}{i}{BikeSerialNumberPostfix}", 
                            BikeType, 
                            manufacturer
                        )
                    );
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
                    Console.WriteLine($"Bike ID: {bike.ID}, Type: {bike.BikeType}");
                }
            }
        }

        /// <summary>
        /// Reads file and fills the ConcurrentQueue with these bikes
        /// 
        /// </summary>
        /// <param name="fileName"> Name of the file to read </param>
        /// <returns></returns>
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
        /// Updates ProgressBar in console accodring to read 
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
        /// Merges all records into a single complete one
        /// </summary>
        private void MergeDictionaryToFile()
        {
            List<Bike> mergedBikes = fileDictionary.Values.SelectMany(q => q).ToList();
            XmlSerializer serializer = new XmlSerializer(typeof(List<Bike>));
            using (var writer = new StreamWriter(allBikesSerialized, false))
            {
                serializer.Serialize(writer, mergedBikes);
            }
        }

        /// <summary>
        /// Starts in background and sorts the Key of the Concurrent Dictionary each second
        /// in alphabetical order of id and prints the result in console
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
                            // sorting by ID from Snapshot
                            var sorted = queue.ToList().OrderBy(b => b.ID.ToString()).ToList();
                            Console.WriteLine($"\n[Sorting] File {key}:");
                            foreach (var bike in sorted)
                            {
                                Console.Write($"[{bike.ID}] ");
                            }
                            Console.WriteLine();
                        }
                    }
                    await Task.Delay(1000); // 1 second delay
                }
            }, sortCts.Token);
        }

        /// <summary>
        /// Stops the sorting
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