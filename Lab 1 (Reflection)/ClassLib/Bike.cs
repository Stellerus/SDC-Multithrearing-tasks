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

        public static Bike Create(int id, string name, string serialNumber, string bikeType, Manufacturer manufacturer)
        {
            return new Bike(id, name, serialNumber, bikeType, manufacturer);
        }

        public void PrintObject()
        {
            Type type = this.GetType();
            if (type.IsClass)
            {
                Console.WriteLine($"Class named {type.Name}");

                foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic))
                {
                    string getAccess = HasGetter(prop);

                    string setAccess = HasSetter(prop);

                    Console.WriteLine($" ({getAccess} Get/{setAccess} Set)  property: {prop.Name} ({prop.PropertyType.Name})");
                }
            }
            string HasGetter(PropertyInfo? prop)
            {
                string getAccess;
                if (prop == null)
                {
                    throw new NullReferenceException();
                }
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
                if (prop == null)
                {
                    throw new NullReferenceException();
                }
                if (prop.GetMethod == null)
                {
                    setAccess = "Missing";
                }
                else if (prop.GetMethod.IsPublic == true)
                {
                    setAccess = "Public";
                }
                else
                {
                    setAccess = "Private";
                }
                return setAccess;
            }
        }
    }

}

