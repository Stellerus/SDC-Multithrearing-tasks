using BikeLibrary;
using System;
using System.Xml.Serialization;


XmlSerializer manufacturerSerializer = new XmlSerializer(typeof(Manufacturer));
XmlSerializer bikeSerializer = new XmlSerializer(typeof(Bike));

Random rnd = new Random();

bool exit = false;


int userChoice = 0;
Type type;
string path = "";
List<object> allObjects = new List<object>();

while (!exit)
{
    Console.WriteLine($"1. Create 10 objects of Type");
    Console.WriteLine($"2. Serialize to XML");
    Console.WriteLine($"3. Show file description");

    switch (userChoice)
    {
        case 1:
            int choice = 0;
            int quantity = 0;
            string? input = Console.ReadLine();
            if (input == null)
            {
                throw new NullReferenceException();
            }
            quantity = int.Parse(input);
            Console.WriteLine($"1. Create {quantity} objects of Bike Type");
            Console.WriteLine($"2. Create {quantity} objects of Manufacturer Type");
            switch (choice)
            {
                case 1:
                    List<Bike> bikeList = CreateBikes(quantity);
                    type = bikeList.GetType();
                    Console.WriteLine($"{quantity} objects of {type.GetType()} created");
                    break;

                case 2:
                    List<Manufacturer> manufacturerList = CreateManufacturers(quantity);
                    type = manufacturerList.GetType();
                    Console.WriteLine($"{quantity} objects of {type.GetType()} created");
                    break;

                default:
                    break;
            }

            ShowDesctiption();
            break;

        case 2:
            SerializeObjects<Bike>();
            SerializeObjects<Manufacturer>();

            Console.WriteLine($"Serialized to {path} path");
            break;

        case 3:
            ShowDesctiption();
            Console.WriteLine();
            break;

        default:
            Console.WriteLine("Exiting");
            break;

    }
}

List<Bike> CreateBikes(int n)
{
    List<Bike> typeList = new List<Bike>(n);
    for (int i = 0; i < typeList.Count; i++)
    {
        Manufacturer defaultManufacturer = new Manufacturer("No Name", "Neverland str. 10", false);
        typeList[i] = new Bike(rnd.Next(), rnd.Next().ToString(), rnd.Next().ToString(), rnd.Next().ToString(), defaultManufacturer );
    }
    return typeList;
}
List<Bike> CreateBikes(List<Manufacturer> existingManufacturers, int n)
{
    List<Bike> typeList = new List<Bike>(n);
    for (int i = 0; i < typeList.Count; i++)
    {
        typeList[i] = new Bike(rnd.Next(), rnd.Next().ToString(), rnd.Next().ToString(), rnd.Next().ToString(),
            existingManufacturers[rnd.Next(0,existingManufacturers.Count)]);
    }
    return typeList;
}

List<Manufacturer> CreateManufacturers(int n)
{
    List<Manufacturer> typeList = new List<Manufacturer>(n);
    for (int i = 0; i < typeList.Count; i++)
    {
        Manufacturer defaultManufacturer = new Manufacturer("No Name", "Neverland str. 10", false);
        typeList[i] = new Manufacturer(rnd.Next().ToString(), rnd.Next().ToString(), Convert.ToBoolean(rnd.Next(0,1)));
    }
    return typeList;
}



void SerializeObjects<T>(List<T> typeList, XmlSerializer typeSerializer)
{
    typeSerializer = new XmlSerializer(typeof(T));

    using (FileStream fs = new FileStream("person.xml", FileMode.OpenOrCreate))
    {
        for (int i = 0; i < typeList.Count; i++)
        {

            typeSerializer.Serialize(fs, typeList[i]);

            Console.WriteLine("Object has been serialized");
        }
    }
}

List<object> DeserealizeObjects(XmlSerializer typeSerializer)
{
    List<object>? typeList;
    using (FileStream fs = new FileStream("person.xml", FileMode.OpenOrCreate))
    {
        typeList = typeSerializer.Deserialize(fs) as List<object>;
        if (typeList == null)
        {
            throw new NullReferenceException("");
        }
    }

    return typeList;
}

void ShowBikes(List<object> list)
{
    foreach (object obj in list)
    {

    }

}

