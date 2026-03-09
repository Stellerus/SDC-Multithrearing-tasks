using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns_Lab1__second_take_.Products
{
    public class Car : Vehicle
    {
        public virtual string WheelDrive { get; set; }
        public virtual string Class { get; set; }
        public virtual string Color { get; set; }

        public override string ToString()
        {
            return base.ToString() +
                   $", WheelDrive: {WheelDrive}, Class: {Class}, Color: {Color}";
        }
    }

}
