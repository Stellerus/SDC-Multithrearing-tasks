using System.Reflection;

Console.WriteLine("Enter Assembly path");
string? assemblyPath = Console.ReadLine();
if (assemblyPath == null)
{
    throw new NullReferenceException("Assembly not found");
}
assemblyPath = assemblyPath.Trim();

//@"C:\Users\petro\Documents\GitHub\SDC-Multithrearing-tasks\Lab 1 (Reflection)\ClassLib\bin\Debug\net6.0\ClassLib.dll" for debug
Assembly assembly = Assembly.LoadFrom(assemblyPath);

//Task 3
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