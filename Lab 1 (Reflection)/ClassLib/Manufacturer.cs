using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BikeLibrary
{
    public class Manufacturer
    {
        public string Name { get; set; }
        public string Address { get; set; }
        private bool IsAChildCompany { get; set; }

        public Manufacturer(string name, string address, bool isAChildCompany)
        {
            Name = name;
            Address = address;
            IsAChildCompany = isAChildCompany;
        }

        public static Manufacturer Create(string name, string address, bool isAChildCompany)
        {
            return new Manufacturer(name, address, isAChildCompany);
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
