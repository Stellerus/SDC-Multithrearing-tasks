using System;
using System.Threading.Tasks;

namespace Lab_5__Concurrency_
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var tool = new ConcurrencyTaskTool();

            Console.WriteLine("=== Task 0: Generating and Saving Bikes ===");
            await tool.GenerateFilesAsync();

            Console.WriteLine("\n=== Task 1: Reading Files Concurrently ===");
            tool.StartSortingHandler();

            await tool.ReadFilesConcurrentlyAsync();


            Console.WriteLine
                (
                    """

                
                    Press any key to stop the sorting handler...


                    """
                );
            Console.ReadKey();
            tool.StopSortingHandler();

            Console.WriteLine("\nSorting handler stopped. Program completed.");
        }
    }
}