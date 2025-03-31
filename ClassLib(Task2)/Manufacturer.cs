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

    }
}
