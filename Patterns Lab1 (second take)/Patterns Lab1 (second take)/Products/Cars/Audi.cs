using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns_Lab1__second_take_.Products.Cars
{
    public class Audi : Car
    {
        public override double Weight { get; set; } = 1400;
        public override double Length { get; set; } = 4.3;
        public override double MaxSpeed { get; set; } = 250;
        public override string WheelDrive { get; set; } = "front";
        public override string Class { get; set; } = "hatchback";
        public override string Color { get; set; } = "blue";
    }
}
