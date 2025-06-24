using BikeLibrary;
using System.Xml.Serialization;

namespace Lab_4__TPL_
{
    public class TaskHandler
    {
        private List<Bike> bikes;

        private const string bikesFirstSerialized = "bikesFirstSerialized.xml";
        private const string bikesSecondSerialized = "bikesSecondSerialized.xml";
        private const string bikesCombinedSerialized = "bikesCombinedSerialized.xml";

        // Manufacturer naming constants
        const string manufacturerStandartName = "Bike Co.";
        const string manufacturerStandartAdress = "123 Street";

        // Bike naming constants
        const string BikeNamePrefix = "Bike_";
        const string BikeSerialNumberPrefix = "SN";
        const string BikeSerialNumberPostfix = ":0000";
        const string BikeType = "Mountain";

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
            Manufacturer manufacturer = Manufacturer.Create(manufacturerStandartName, manufacturerStandartAdress, false);

            List<Bike> list = new List<Bike>();

            for (int i = 1; i <= 20; i++)
            {
                list.Add(Bike.Create(i, $"{BikeNamePrefix}{i}", $"{BikeSerialNumberPrefix}{i}{BikeSerialNumberPostfix}", BikeType, manufacturer));
            }

            Console.WriteLine("Generated 10 bikes");

            return list;
        }

        /// <summary>
        /// Serializes 10 instances to file1.xml
        /// and 10 instances to file2.xml in parallel
        /// </summary>
        public async Task SerializeBikesAsync()
        {
            //Takes 10 first bikes and converts to list for serialization
            Task serializingTask1 = Task.Run(() =>
                SerializeAndSave(bikes.Take(10).ToList(), bikesFirstSerialized));

            // Skips already serialized bikes and converts another 10 to list for serialization
            Task serializingTask2 = Task.Run(() =>
                SerializeAndSave(bikes.Skip(10).Take(10).ToList(), bikesSecondSerialized)); 

            await Task.WhenAll(serializingTask1, serializingTask2);

            Console.WriteLine("Bikes Serialized");
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
                Task readWriteTask1 = Task.Run(() => ReadAndWrite(bikesFirstSerialized));
                Task readWriteTask2 = Task.Run(() => ReadAndWrite(bikesSecondSerialized));

                await Task.WhenAll(readWriteTask1, readWriteTask2);

                Console.WriteLine($"Merged {bikesFirstSerialized} with {bikesSecondSerialized} to {bikesCombinedSerialized}");
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
                    try
                    {
                        XmlSerializer serializerBike = new XmlSerializer(typeof(Bike));
                        using (StreamWriter writer = new StreamWriter(bikesCombinedSerialized, true))
                        {
                            serializerBike.Serialize(writer, bike);
                        }
                    }
                    catch (Exception exInner)
                    {
                        Console.WriteLine($"Error writing bike {bike.ID} from file [{filename}]: {exInner.Message}");
                        throw;
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
                string[] lines = File.ReadAllLines(bikesCombinedSerialized);

                // Divide file in two (Get the line in the middle of file)
                int mid = lines.Length / 2;

                Task printingTask1 = Task.Run(() => PrintLines(lines, 0, mid));
                Task printingTask2 = Task.Run(() => PrintLines(lines, mid, lines.Length));

                await Task.WhenAll(printingTask1, printingTask2);

                Console.WriteLine("Reading with two tasks successful");
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