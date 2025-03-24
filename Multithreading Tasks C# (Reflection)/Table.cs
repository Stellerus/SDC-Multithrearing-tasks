using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Furniture
{
    internal class Table
    {
        public int Legs { get; private set; }
        public float Slope { get; private set; }

        public Table()
        {
            Legs = 4;
            Slope = 0;
        }
        //public Table(int legs)
        //{
        //    Legs = legs;
        //    Slope = 0;
        //}
        //public Table(int legs, float slope)
        //{
        //    Legs = legs;
        //    Slope = slope;
        //}

        public void Stand()
        {
            Console.WriteLine("Standing");
        }
    }
}
