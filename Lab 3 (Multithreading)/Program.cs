using Lab_3__Multithreading_;

var threadHandler = new ThreadHandler();


bool exit = false;

while (!exit)
{
    //Menu
    Console.WriteLine("\n--- Main Menu ---");
    Console.WriteLine("1. Serialize Bikes");
    Console.WriteLine("2. Merge into single file");
    Console.WriteLine("3. Read result in 1 thread");
    Console.WriteLine("4. Read result with 2 threads");
    Console.WriteLine("5. Read result with 10 threads");
    Console.WriteLine("0. Exit");

    Console.Write("Select an option: ");
    string? menuChoice = Console.ReadLine();

    switch (menuChoice)
    {
        case "1":
            threadHandler.SerializeBikes();
            break;
        case "2":
            threadHandler.MergeFiles();
            break;
        case "3":
            threadHandler.ReadResultFileSingleThread();
            break;
        case "4":
            threadHandler.ReadResultFileTwoThreads();
            break;
        case "5":
            threadHandler.ReadResultFileTenThreads();
            break;
        case "0":
            exit = true;
            break;
        default:
            Console.WriteLine("Invalid choice.");
            break;
    }
}

