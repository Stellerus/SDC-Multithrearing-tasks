using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Manufacturer
    {
        public string Name { get; set; }
        public int Id { get; set; }


        public Manufacturer(){}
        public Manufacturer(string name)
        {
            Name = name;
        }
    }
}
