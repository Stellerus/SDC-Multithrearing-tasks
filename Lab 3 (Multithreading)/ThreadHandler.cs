using BikeLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lab_3__Multithreading_
{

    public class ThreadHandler
    {
        private List<Bike> bikes;

        private const string file1 = "file1.xml";
        private const string file2 = "file2.xml";
        private const string resultFile = "resultFile.xml";

        private object monitorLocker = new object();

        // 5 initial and 5 max threads in semaphore at the same time
        const int semaphoreCount = 5;
        private Semaphore semaphore = new Semaphore(semaphoreCount, semaphoreCount);

        public ThreadHandler()
        {
            bikes = GenerateBikes();
        }

        /// <summary>
        /// Generates 20 sample bike instances with the same manufacturer
        /// </summary>
        /// <returns> List of Bikes</returns>
        private List<Bike> GenerateBikes()
        {

            // Manufacturer naming constants
            const string manufacturerStandartName = "Bike Co.";
            const string manufacturerStandartAdress = "123 Street";

            Manufacturer manufacturer = Manufacturer.Create(manufacturerStandartName, manufacturerStandartAdress, false);

            // Bike naming constants
            const string BikeNamePrefix = $"Bike_";
            const string BikeSerialNumberPrefix = "SN";
            const string BikeSerialNumberPostfix = ":0000";
            const string BikeType = "Mountain";

            List<Bike> list = new List<Bike>();

            for (int i = 1; i <= 20; i++)
            {
                list.Add(Bike.Create(i, $"{BikeNamePrefix}{i}", $"{BikeSerialNumberPrefix}{i}{BikeSerialNumberPostfix}", BikeType, manufacturer));
            }

            return list;
        }

        /// <summary>
        /// Serializes Bikes to a file with two parallel threads
        /// </summary>
        public void SerializeBikes()
        {
            Thread t1 = new Thread(() => SerializeAndSave(bikes.Take(10).ToList(), file1));
            Thread t2 = new Thread(() => SerializeAndSave(bikes.Skip(10).Take(10).ToList(), file2));

            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();
        }

        /// <summary>
        /// Serializes list of Bikes to file using XmlSerializer
        /// </summary>
        /// <param name="bikesToSave"> List of Bikes</param>
        /// <param name="filename"> Name of a resulting file</param>
        private void SerializeAndSave(List<Bike> bikesToSave, string filename)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Bike>));
                using (StreamWriter writer = new StreamWriter(filename))
                {
                    serializer.Serialize(writer, bikesToSave);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error serializing to {filename}: {ex.Message}");
            }

        }

        /// <summary>
        /// Reads both files and writes their respective data to single resultFile
        /// </summary>
        public void MergeFiles()
        {
            Thread t1 = new Thread(() => ReadAndWrite(file1));
            Thread t2 = new Thread(() => ReadAndWrite(file2));

            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();
        }

        /// <summary>
        /// Reads from filename and serializes to a file stated in resultFile constant
        /// </summary>
        /// <param name="filename"> Name of a file to read</param>
        private void ReadAndWrite(string filename)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Bike>));
                List<Bike>? content;

                using (StreamReader reader = new StreamReader(filename))
                {
                    content = (List<Bike>?)serializer.Deserialize(reader);
                }

                if (content == null) return;

                foreach (var bike in content)
                {
                    Monitor.Enter(monitorLocker);
                    try
                    {
                        XmlSerializer serializerBike = new XmlSerializer(typeof(Bike));
                        using (StreamWriter writer = new StreamWriter(resultFile, true))
                        {
                            serializerBike.Serialize(writer, bike);
                        }
                    }
                    finally
                    {
                        Monitor.Exit(monitorLocker);
                    }
                    Thread.Sleep(100);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading/writing file {filename}: {ex.Message}");
            }
        }

        /// <summary>
        /// Reads resultFile in single thread and shows read time
        /// </summary>
        public void ReadResultFileSingleThread()
        {
            System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();

            string[] lines;
            try
            {
                lines = File.ReadAllLines(resultFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file {resultFile}: {ex.Message}");
                return;
            }

            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }

            stopwatch.Stop();
            Console.WriteLine($"Single thread read time: {stopwatch.ElapsedMilliseconds} ms");
        }

        /// <summary>
        /// Reads resultFile with two threads and shows read time
        /// </summary>
        public void ReadResultFileTwoThreads()
        {
            System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();

            string[] lines;
            try
            {
                lines = File.ReadAllLines(resultFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file {resultFile}: {ex.Message}");
                return;
            }

            int mid = lines.Length / 2;

            const int startingLine = 0;
            Thread t1 = new Thread(() => PrintLines(lines, startingLine, mid));
            Thread t2 = new Thread(() => PrintLines(lines, mid, lines.Length));

            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();

            stopwatch.Stop();
            Console.WriteLine($"Two threads read time: {stopwatch.ElapsedMilliseconds} ms");
        }

        /// <summary>
        /// Prints lines of array between start and end values
        /// </summary>
        /// <param name="lines"> Array of lines to print</param>
        /// <param name="start"> Number of starting line</param>
        /// <param name="end"> Number of ending line</param>
        private void PrintLines(string[] lines, int start, int end)
        {
            for (int i = start; i < end; i++)
            {
                Console.WriteLine(lines[i]);
            }
        }

        /// <summary>
        /// Reads resultFile with ten threads (5 simultaneously) and shows read time
        /// </summary>
        public void ReadResultFileTenThreads()
        {
            System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();

            string[] lines;
            try
            {
                lines = File.ReadAllLines(resultFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file {resultFile}: {ex.Message}");
                return;
            }

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
            Console.WriteLine($"Ten threads (5 simultaneously) read time: {stopwatch.ElapsedMilliseconds} ms");
        }

        /// <summary>
        /// Prints lines of array between start and end values
        /// Uses a semaphore and releases threads in the end
        /// </summary>
        /// <param name="lines"> Array of lines to print</param>
        /// <param name="start"> Number of starting line</param>
        /// <param name="end"> Number of ending line</param>
        private void ReadWithSemaphore(string[] lines, int start, int end)
        {
            semaphore.WaitOne();

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
