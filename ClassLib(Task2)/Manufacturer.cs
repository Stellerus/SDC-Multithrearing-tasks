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

        public Manufacturer Create(string name, string adress, bool isAChildCompany)
        {
            return new Manufacturer(Name, Address, IsAChildCompany);
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
            }
        }
    }
}
