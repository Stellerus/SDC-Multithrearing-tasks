using BikeLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;


List<Bike> bikeList = new List<Bike>();
List<Manufacturer> manufacturerList = new List<Manufacturer>();
Random rnd = new Random();
string bikeXmlPath = "bikes.xml";
string manufacturerXmlPath = "manufacturers.xml";

bool exit = false;

while (!exit)
{
    Console.WriteLine("\n--- Main Menu ---");
    Console.WriteLine("1. Create objects");
    Console.WriteLine("2. Serialize objects to XML");
    Console.WriteLine("3. Show XML file contents");
    Console.WriteLine("4. Deserialize and display");
    Console.WriteLine("5. Show all 'Model' values (XDocument)");
    Console.WriteLine("6. Show all 'Model' values (XmlDocument)");
    Console.WriteLine("7. Modify Bike value (XDocument)");
    Console.WriteLine("8. Modify Bike value (XmlDocument)");
    Console.WriteLine("0. Exit");

    Console.Write("Select an option: ");
    string? menuChoice = Console.ReadLine();

    switch (menuChoice)
    {
        case "1":
            CreateObjectsMenu();
            break;
        case "2":
            SerializeObjects();
            break;
        case "3":
            ShowFileContents();
            break;
        case "4":
            DeserializeAndShow();
            break;
        case "5":
            XmlUtility.ShowBikeValuesXDocument();
            break;
        case "6":
            XmlUtility.ShowBikeValuesXmlDocument();
            break;
        case "7":
            ModifyElement(true);
            break;
        case "8":
            ModifyElement(false);
            break;

        case "0":
            exit = true;
            break;
        default:
            Console.WriteLine("Invalid choice.");
            break;
    }
}

void CreateObjectsMenu()
{
    Console.Write("How many objects to create? ");
    if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
    {
        Console.WriteLine("Invalid number.");
        return;
    }

    Console.WriteLine("1. Create Bike objects");
    Console.WriteLine("2. Create Manufacturer objects");

    Console.Write("Choose type: ");
    string? typeChoice = Console.ReadLine();

    switch (typeChoice)
    {
        case "1":
            bikeList = CreateBikes(quantity);
            Console.WriteLine($"{quantity} Bike objects created.");
            break;
        case "2":
            manufacturerList = CreateManufacturers(quantity);
            Console.WriteLine($"{quantity} Manufacturer objects created.");
            break;
        default:
            Console.WriteLine("Invalid choice.");
            break;
    }
}

List<Bike> CreateBikes(int n)
{
    var list = new List<Bike>();
    for (int i = 0; i < n; i++)
    {
        var manufacturer = new Manufacturer($"Name {i}", $"Adress {i}", rnd.Next(0, 2) == 1);
        list.Add(new Bike(
            id: rnd.Next(1000, 9999),
            name: $"BikeName{i}",
            serialNumber: $"SN{i:D6}",
            bikeType: $"Type{i % 3}",
            manufacturer: manufacturer));
    }
    return list;
}

List<Manufacturer> CreateManufacturers(int n)
{
    var list = new List<Manufacturer>();
    for (int i = 0; i < n; i++)
    {
        list.Add(new Manufacturer($"Name {i}", $"Adress {i}", rnd.Next(0, 2) == 1));
    }
    return list;
}

void SerializeObjects()
{
    if (bikeList.Count > 0)
    {
        XmlSerializer bikeSerializer = new XmlSerializer(typeof(List<Bike>));
        using (FileStream fs = new FileStream(bikeXmlPath, FileMode.Create))
        {
            bikeSerializer.Serialize(fs, bikeList);
        }
        Console.WriteLine("Bike list serialized to XML.");
    }

    if (manufacturerList.Count > 0)
    {
        XmlSerializer manufacturerSerializer = new XmlSerializer(typeof(List<Manufacturer>));
        using (FileStream fs = new FileStream(manufacturerXmlPath, FileMode.Create))
        {
            manufacturerSerializer.Serialize(fs, manufacturerList);
        }
        Console.WriteLine("Manufacturer list serialized to XML.");
    }
}

void ShowFileContents()
{
    if (File.Exists(bikeXmlPath))
    {
        Console.WriteLine("\n--- bikes.xml ---");
        Console.WriteLine(File.ReadAllText(bikeXmlPath));
    }

    if (File.Exists(manufacturerXmlPath))
    {
        Console.WriteLine("\n--- manufacturers.xml ---");
        Console.WriteLine(File.ReadAllText(manufacturerXmlPath));
    }
}

void DeserializeAndShow()
{
    if (File.Exists(bikeXmlPath))
    {
        XmlSerializer bikeSerializer = new XmlSerializer(typeof(List<Bike>));
        using (FileStream fs = new FileStream(bikeXmlPath, FileMode.Open))
        {
            var bikes = (List<Bike>)bikeSerializer.Deserialize(fs);
            Console.WriteLine("\n--- Deserialized Bikes ---");
            foreach (var b in bikes)
            {
                Console.WriteLine(b);
            }
        }
    }

    if (File.Exists(manufacturerXmlPath))
    {
        XmlSerializer manufacturerSerializer = new XmlSerializer(typeof(List<Manufacturer>));
        using (FileStream fs = new FileStream(manufacturerXmlPath, FileMode.Open))
        {
            var manufacturers = (List<Manufacturer>)manufacturerSerializer.Deserialize(fs);
            Console.WriteLine("\n--- Deserialized Manufacturers ---");
            foreach (var m in manufacturers)
            {
                Console.WriteLine(m);
            }
        }
    }
}
void ModifyElement(bool useXDocument)
{
    Console.Write("Enter element name to modify (e.g., Name, Type): ");
    string? elementName = Console.ReadLine();

    Console.Write("Enter object index (0-based): ");
    if (!int.TryParse(Console.ReadLine(), out int index))
    {
        Console.WriteLine("Invalid index.");
        return;
    }

    Console.Write("Enter new value: ");
    string? newValue = Console.ReadLine();

    if (string.IsNullOrEmpty(elementName) || string.IsNullOrEmpty(newValue))
    {
        Console.WriteLine("Invalid input.");
        return;
    }

    if (useXDocument)
        XmlUtility.ModifyWithXDocument(elementName, index, newValue);
    else
        XmlUtility.ModifyWithXmlDocument(elementName, index, newValue);
}

