using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns_Lab1__second_take_.Products
{
    public abstract class Vehicle
    {
        public virtual double Weight { get; set; }
        public virtual double Length { get; set; }
        public virtual double MaxSpeed { get; set; }

        public override string ToString()
        {
            return $"Weight: {Weight}, Length: {Length}, MaxSpeed: {MaxSpeed}";
        }
    }
}
