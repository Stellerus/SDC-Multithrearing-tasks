using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BikeLibrary;

namespace Lab_3__Multithreading_
{

    public class BikeProcessor
    {
        private List<Bike> bikes;
        private string file1 = "file1.json";
        private string file2 = "file2.json";
        private string resultFile = "resultFile.json";
        private object locker = new object();
        private SemaphoreSlim semaphore = new SemaphoreSlim(5);

        public BikeProcessor()
        {
            bikes = GenerateBikes();
        }

        private List<Bike> GenerateBikes()
        {
            var manufacturer = Manufacturer.Create("SuperBike Co.", "123 Bike Street", false);
            var list = new List<Bike>();

            for (int i = 1; i <= 20; i++)
            {
                list.Add(Bike.Create(i, $"Bike_{i}", $"SN{i:0000}", "Mountain", manufacturer));
            }

            return list;
        }

        public void SerializeBikes()
        {
            Thread t1 = new Thread(() => SerializeAndSave(bikes.Take(10).ToList(), file1));
            Thread t2 = new Thread(() => SerializeAndSave(bikes.Skip(10).Take(10).ToList(), file2));

            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();
        }

        private void SerializeAndSave(List<Bike> bikesToSave, string filename)
        {
            var json = JsonSerializer.Serialize(bikesToSave, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filename, json);
        }

        public void MergeFiles()
        {
            Thread t1 = new Thread(() => ReadAndWrite(file1));
            Thread t2 = new Thread(() => ReadAndWrite(file2));

            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();
        }

        private void ReadAndWrite(string filename)
        {
            var content = JsonSerializer.Deserialize<List<Bike>>(File.ReadAllText(filename));
            if (content == null) return;

            foreach (var bike in content)
            {
                lock (locker)
                {
                    var json = JsonSerializer.Serialize(bike, new JsonSerializerOptions { WriteIndented = true });
                    File.AppendAllText(resultFile, json + Environment.NewLine);
                }
                Thread.Sleep(100);
            }
        }

        public void ReadResultFileSingleThread()
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            var lines = File.ReadAllLines(resultFile);
            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }

            stopwatch.Stop();
            Console.WriteLine($"Single thread read time: {stopwatch.ElapsedMilliseconds} ms");
        }

        public void ReadResultFileTwoThreads()
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            var lines = File.ReadAllLines(resultFile);
            int mid = lines.Length / 2;

            Thread t1 = new Thread(() => PrintLines(lines, 0, mid));
            Thread t2 = new Thread(() => PrintLines(lines, mid, lines.Length));

            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();

            stopwatch.Stop();
            Console.WriteLine($"Two threads read time: {stopwatch.ElapsedMilliseconds} ms");
        }

        private void PrintLines(string[] lines, int start, int end)
        {
            for (int i = start; i < end; i++)
            {
                Console.WriteLine(lines[i]);
            }
        }

        public void ReadResultFileTenThreads()
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            var lines = File.ReadAllLines(resultFile);
            int portion = lines.Length / 10;

            List<Thread> threads = new List<Thread>();

            for (int i = 0; i < 10; i++)
            {
                int start = i * portion;
                int end = (i == 9) ? lines.Length : start + portion;

                Thread thread = new Thread(() => ReadWithSemaphore(lines, start, end));
                threads.Add(thread);
            }

            foreach (var thread in threads)
            {
                thread.Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

            stopwatch.Stop();
            Console.WriteLine($"Ten threads (with 5 concurrency) read time: {stopwatch.ElapsedMilliseconds} ms");
        }

        private void ReadWithSemaphore(string[] lines, int start, int end)
        {
            semaphore.Wait();

            try
            {
                for (int i = start; i < end; i++)
                {
                    Console.WriteLine(lines[i]);
                }
            }
            finally
            {
                semaphore.Release();
            }
        }
    }
}
