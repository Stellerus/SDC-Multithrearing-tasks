using BikeLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lab_4__TPL_
{
    public class TaskHandler
    {
        private List<Bike> bikes;

        private const string file1 = "file1.xml";
        private const string file2 = "file2.xml";
        private const string resultFile = "resultFile.xml";

        private object monitorLocker = new object();

        public TaskHandler()
        {
            bikes = GenerateBikes();
        }

        /// <summary>
        /// Generates 20 sample bike instances with the same manufacturer
        /// </summary>
        /// <returns> List of Bike</returns>
        private List<Bike> GenerateBikes()
        {
            // Manufacturer naming constants
            const string manufacturerStandartName = "Bike Co.";
            const string manufacturerStandartAdress = "123 Street";

            Manufacturer manufacturer = Manufacturer.Create(manufacturerStandartName, manufacturerStandartAdress, false);

            // Bike naming constants
            const string BikeNamePrefix = "Bike_";
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
        /// Serializes 10 instances to file1.xml
        /// and 10 instances to file2.xml in parallel
        /// </summary>
        public async Task SerializeBikesAsync()
        {
            Task task1 = Task.Run(() => SerializeAndSave(bikes.Take(10).ToList(), file1));
            Task task2 = Task.Run(() => SerializeAndSave(bikes.Skip(10).Take(10).ToList(), file2));

            await Task.WhenAll(task1, task2);
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
                Console.WriteLine($"Error serializing bikes to file [{filename}]: {ex.Message}");
                throw;
            }

        }

        /// <summary>
        /// Reads both files and writes their respective data to single resultFile
        /// </summary>
        public async Task MergeFilesAsync()
        {
            try
            {
                Task task1 = Task.Run(() => ReadAndWrite(file1));
                Task task2 = Task.Run(() => ReadAndWrite(file2));

                await Task.WhenAll(task1, task2);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error merging files: {ex.Message}");
                throw;
            }

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

            if (content == null)
                return;

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
                    catch (Exception exInner)
                    {
                        Console.WriteLine($"Error writing bike {bike.ID} from file [{filename}]: {exInner.Message}");
                        throw;
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
                Console.WriteLine($"Error reading or processing file [{filename}]: {ex.Message}");
                throw;
            }

        }

        /// <summary>
        /// Reads and prints resultFile in two parallel tasks
        /// </summary>
        public async Task ReadResultFileTwoTasksAsync()
        {
            try
            {
                string[] lines = File.ReadAllLines(resultFile);
                int mid = lines.Length / 2;

                Task task1 = Task.Run(() => PrintLines(lines, 0, mid));
                Task task2 = Task.Run(() => PrintLines(lines, mid, lines.Length));

                await Task.WhenAll(task1, task2);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading result file in two tasks: {ex.Message}");
                throw;
            }

        }

        /// <summary>
        /// Prints lines of array between start and end values
        /// </summary>
        /// <param name="lines"> Array of lines to print</param>
        /// <param name="start"> Number of starting line</param>
        /// <param name="end"> Number of ending line</param>
        private void PrintLines(string[] lines, int start, int end)
        {
            try
            {
                for (int i = start; i < end; i++)
                {
                    Console.WriteLine(lines[i]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error printing lines: {ex.Message}");
            }

        }
    }
}