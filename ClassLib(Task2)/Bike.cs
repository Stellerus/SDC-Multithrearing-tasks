using System.Reflection;

namespace BikeLibrary
{
    public class Bike
    {
        private int ID { get; set; }
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public string BikeType { get; set; }
        public Manufacturer Manufacturer { get; set; }

        public Bike(int id, string name, string serialNumber, string bikeType, Manufacturer manufacturer)
        {
            ID = id;
            Name = name;
            SerialNumber = serialNumber;
            BikeType = bikeType;
            Manufacturer = manufacturer;
        }
        public Bike Create(int id, string name, string serialNumber, string bikeType, Manufacturer manufacturer)
        {
            return new Bike(id, name, serialNumber, bikeType, manufacturer);
        }

        public void PrintObject()
        {
            var type = typeof(Bike);
            if (type.IsClass)
            {
                Console.WriteLine($"Class named {type.Name}");

                foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic))
                {
                    string getAccess;
                    if (prop.GetMethod == null)
                    {
                        throw new NullReferenceException();
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
                        throw new NullReferenceException();
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
                }

            //    foreach (var field in type.GetFields())
            //    {
            //        string access;

            //        if (field.IsPublic == true)
            //        {
            //            access = "Public";
            //        }
            //        else
            //        {
            //            access = "Private";
            //        }

            //        Console.WriteLine($" {access} field: {field.Name} ({field.FieldType.Name})");
            //    }

            //    foreach (var ctor in type.GetConstructors())
            //    {
            //        Console.WriteLine($" Constructor: {ctor.Name}");

            //        foreach (var parameter in ctor.GetParameters())
            //        {
            //            Console.WriteLine($" Parameter: ({parameter.ParameterType.Name}) {parameter.Name} ");
            //        }

            //    }
            //    Console.WriteLine();

            //    foreach (var method in type.GetMethods())
            //    {
            //        string access;

            //        if (method.IsPublic == true)
            //        {
            //            access = "Public";
            //        }
            //        else
            //        {
            //            access = "Private";
            //        }

            //        Console.WriteLine($" {access} Method: {method.ReturnParameter.ParameterType.Name} {method.Name}");

            //        foreach (var parameter in method.GetParameters())
            //        {
            //            Console.WriteLine($" Parameter: ({parameter.ParameterType.Name}) {parameter.Name} ");
            //        }

            //    }
            //    Console.WriteLine();
            }

        }
    }
}

