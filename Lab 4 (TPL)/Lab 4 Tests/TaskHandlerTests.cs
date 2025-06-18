using BikeLibrary;
using Lab_4__TPL_;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TaskHandlerTests
{
    [TestFixture]
    public class TaskHandlerTests
    {
        private const string file1 = "file1.xml";
        private const string file2 = "file2.xml";
        private const string resultFile = "resultFile.xml";

        [SetUp]
        public void Setup()
        {
            if (File.Exists(file1))
                File.Delete(file1);
            if (File.Exists(file2))
                File.Delete(file2);
            if (File.Exists(resultFile))
                File.Delete(resultFile);
        }

        [Test]
        public async Task SerializeBikesAsync_CreatesTwoFilesWith10BikesEach()
        {
            TaskHandler handler = new TaskHandler();

            await handler.SerializeBikesAsync();

            Assert.IsTrue(File.Exists(file1), "file1.xml has to be created");
            Assert.IsTrue(File.Exists(file2), "file2.xml has to be created");

            XmlSerializer serializer = new XmlSerializer(typeof(List<Bike>));

            List<Bike> bikesFile1;
            using (StreamReader reader = new StreamReader(file1))
            {
                bikesFile1 = (List<Bike>)serializer.Deserialize(reader);
            }

            List<Bike> bikesFile2;
            using (StreamReader reader = new StreamReader(file2))
            {
                bikesFile2 = (List<Bike>)serializer.Deserialize(reader);
            }

            Assert.That(bikesFile1, Has.Count.EqualTo(10), "file1.xml must have 10 instances of Bike");
            Assert.That(bikesFile2, Has.Count.EqualTo(10), "file2.xml must have 10 instances of Bike");
        }

        [Test]
        public async Task MergeFilesAsync_MergesTwoFilesIntoResultFile()
        {
            var handler = new TaskHandler();
            await handler.SerializeBikesAsync();

            await handler.MergeFilesAsync();

            Assert.IsTrue(File.Exists(resultFile), "resultFile.xml has to be created");

            string content = File.ReadAllText(resultFile);
            int count = CountOccurrences(content, "<Bike");
            Assert.That(count, Is.EqualTo(40), "Result file must have 20 instances (2 counts per instance) of bike");
        }

        [Test]
        public async Task ReadResultFileTwoThreadsAsync_CompletesSuccessfully()
        {
            var handler = new TaskHandler();
            await handler.SerializeBikesAsync();
            await handler.MergeFilesAsync();

            Assert.DoesNotThrowAsync(async () => await handler.ReadResultFileTwoTasksAsync());
        }

        private int CountOccurrences(string text, string substring)
        {
            int count = 0;
            int index = 0;
            while ((index = text.IndexOf(substring, index)) != -1)
            {
                count++;
                index += substring.Length;
            }
            return count;
        }
    }
}