using Lab_4__TPL_;

class Program
{
    static async Task Main(string[] args)
    {
        var threadHandler = new TaskHandler();

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\n--- Main Menu ---");
            Console.WriteLine("1. Serialize Bikes to XML Files");
            Console.WriteLine("2. Merge XML Files into Result File");
            Console.WriteLine("3. Display Result File Asynchronously");
            Console.WriteLine("0. Exit");
            Console.Write("Select an option: ");

            string? menuChoice = Console.ReadLine();

            switch (menuChoice)
            {
                case "1":
                    await threadHandler.SerializeBikesAsync();
                    break;
                case "2":
                    await threadHandler.MergeFilesAsync();
                    break;
                case "3":
                    await threadHandler.ReadResultFileTwoTasksAsync();
                    break;
                case "0":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }
    }
}