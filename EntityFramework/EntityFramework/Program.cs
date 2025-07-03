using ClassLibrary;
using EntityFramework;
using ExamTask;
using System;
using System.Collections.Generic;

class Program
{
    //Number to Generate
    const int ManufacturersNumber = 5;
    const int TablesNumber = 200;

    static async Task Main()
    {
        bool running = true;

        List<Manufacturer>? manufacturers = null;
        List<Table>? tables = null;

        while (running)
        {
            Console.WriteLine("\nВыберите действие:");
            Console.WriteLine("1. Generate");
            Console.WriteLine("2. Serialize");
            Console.WriteLine("3. Read and Deserialize");
            Console.WriteLine("4. Exit");

            string input = Console.ReadLine();

            
            switch (input)
            {
                case "1":
                    Generate(tables, manufacturers);
                    break;

                case "2":
                    if (tables is null || manufacturers is null)
                    {
                        Console.WriteLine("Data not generated. Run Generate first");
                        continue;
                    }
                    Serialize(tables, manufacturers);

                    break;

                case "3":
                    Console.WriteLine("Deserializing");
                    
                    break;

                case "4":
                    Exit(running);
                    break;

                default:
                    Console.WriteLine("Try again");
                    break;
            }
        }
        

        
    }

    static void Generate(List<Table>? tables, List<Manufacturer>? manufacturers)
    {
        Console.WriteLine("Generating");

        manufacturers = Generator.GenerateManufacturers(ManufacturersNumber);
        tables = Generator.GenerateTables(TablesNumber, manufacturers);
    }

    static void Serialize(List<Table>? tables, List<Manufacturer>? manufacturers)
    {
        Console.WriteLine("Serializing");
        
        Serializer.SerializeAllXML(tables, manufacturers);
    }

    void Deserialize()
    {

    }

    static void Exit(bool running)
    {
        Console.WriteLine("Exiting");
        running = false;
    }




    static async Task EntityFunctionality()
    {
        var manufacturers = Serializer.Deserialize<Manufacturer>("manufacturers.xml");
        var products = Serializer.Deserialize<Table>("products.xml");

        using var db = new ProductContext();
        await db.Database.EnsureCreatedAsync();

        await db.Manufacturers.AddRangeAsync(manufacturers);
        await db.SaveChangesAsync();

        await db.Tables.AddRangeAsync(products);
        await db.SaveChangesAsync();

    }

}