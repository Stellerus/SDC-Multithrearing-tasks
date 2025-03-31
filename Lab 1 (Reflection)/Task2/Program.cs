using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;




//Task 2
Assembly assembly = Assembly.LoadFrom(@"C:\Users\petro\Documents\GitHub\SDC-Multithrearing-tasks\ClassLib(Task2)\bin\Debug\net6.0\ClassLib(Task2).dll");

ShowProperties();


try
{
    Type bikeType = assembly.GetType("BikeLibrary.Bike")!;
    Type manufacturerType = assembly.GetType("BikeLibrary.Manufacturer")!;


    //Created manufacturer
    MethodInfo manufacturerCreate = manufacturerType.GetMethod("Create", BindingFlags.Static | BindingFlags.Public)!;
    object manufacturer = manufacturerCreate.Invoke(null, new object[] { "FactoryName", "USA", false })!;

    Console.WriteLine("Manufacturer instance created successfully");


    //Created bike
    MethodInfo bikeCreate = bikeType.GetMethod("Create", BindingFlags.Static | BindingFlags.Public)!;
    object bike = bikeCreate.Invoke(null, new object[] { 1, "BikeName", "SN12345", "Mountain", manufacturer })!;

    Console.WriteLine("Bike instance created successfully.");

    //Print manufacturer
    MethodInfo printManufacturerMethod = manufacturerType.GetMethod("PrintObject", BindingFlags.Instance | BindingFlags.Public)!;
    printManufacturerMethod.Invoke(manufacturer, null);

    //Print bike
    MethodInfo printBikeMethod = bikeType.GetMethod("PrintObject", BindingFlags.Instance | BindingFlags.Public)!;
    printBikeMethod.Invoke(bike, null);
    

}
catch (FileNotFoundException ex)
{
    Console.WriteLine($"Assembly file not found - {ex.Message}");
}
catch (TargetInvocationException ex)
{
    Console.WriteLine($"Method invocation failed - {ex.InnerException?.Message}");
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Incorrect method parameters - {ex.Message}");
}
catch (NullReferenceException ex)
{
    Console.WriteLine($"A required type or method was not found - {ex.Message}");
}
catch (Exception ex)
{
    Console.WriteLine($"General Error: {ex.Message}");
}




object GetInstance(Type? type)
{
    if (type == null)
    {
        throw new NullReferenceException("Type not found");
    }
    object? Instance = Activator.CreateInstance(type);
    if (Instance == null)
    {
        throw new NullReferenceException("Could not create instance");
    }
    return Instance;
}
string HasGetter(PropertyInfo? prop)
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
    return getAccess;
}
string HasSetter(PropertyInfo? prop)
{
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
    return setAccess;
}

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
                string getAccess = string.Empty;
                getAccess = HasGetter(prop);
                
                string setAccess = string.Empty;
                setAccess = HasSetter(prop);

                Console.WriteLine($" ({getAccess} Get/{setAccess} Set)  property: {prop.Name} ({prop.PropertyType.Name})");

                Console.WriteLine();
            }

        }

    }
}
