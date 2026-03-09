using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns_Lab1__second_take_.Products.Tanks
{
    public class Abrams : Tank
    {
        public override double Weight { get; set; } = 9400;
        public override double Length { get; set; } = 4.3;
        public override double MaxSpeed { get; set; } = 50;
        public override double ProjectileCaliber { get; set; } = 0.35;
        public override int ShotsPerMinute { get; set; } = 10;
        public override int CrewSize { get; set; } = 5;
    }
}
