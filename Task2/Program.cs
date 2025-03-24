using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;


//Task 2
Assembly assembly = Assembly.LoadFrom(@"C:\Users\petro\Documents\GitHub\SDC-Multithrearing-tasks\Multithreading Tasks C# (Reflection)\ClassLib(Task2)\bin\Debug\net6.0\ClassLib(Task2).dll");

ShowProperties();

//Task 3
Type? bikeType = assembly.GetType("BikeLibrary.Bike");
Type? manufacturerType = assembly.GetType("BikeLibrary.Manufacturer");

if (bikeType == null || manufacturerType == null)
{
    throw new Exception("Classes not found in assembly.");
}

MethodInfo? manufacturerCreate = manufacturerType.GetMethod("Create");
object? manufacturerInstance = manufacturerCreate.Invoke(null, new object[] { "FactoryName", "USA", false });

MethodInfo? bikeCreate = bikeType.GetMethod("Create");
object? bikeInstance = bikeCreate.Invoke(null, new object[] { "Trek 5000", "SN12345", "Mountain", manufacturerInstance });


void ShowProperties()
{
    foreach (var type in assembly.GetTypes())
    {
        if (type.IsClass)
        {
            Console.WriteLine($"Class named {type.Name}");
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);

            foreach (var prop in properties)
            {
                string getAccess;
                if (prop.GetMethod == null)
                {
                    getAccess = "Missing";
                }
                else if (prop.GetMethod.IsPublic == true)
                {
                    getAccess = "Public";
                }
                else
                {
                    getAccess = "Private";
                }

                string setAccess;
                if (prop.SetMethod == null)
                {
                    setAccess = "Missing";
                }
                else if (prop.SetMethod.IsPublic == true)
                {
                    setAccess = "Public";
                }
                else
                {
                    setAccess = "Private";
                }

                Console.WriteLine($" ({getAccess} Get/{setAccess} Set)  property: {prop.Name} ({prop.PropertyType.Name})");
                Console.WriteLine();
            }

        }

    }
}
