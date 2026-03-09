using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns_Lab1__second_take_.Products.Cars
{
    public class Tesla : Car
    {
        public override double Weight { get; set; } = 1940;
        public override double Length { get; set; } = 5;
        public override double MaxSpeed { get; set; } = 200;
        public override string WheelDrive { get; set; } = "front";
        public override string Class { get; set; } = "coupe";
        public override string Color { get; set; } = "red";
    }
}
