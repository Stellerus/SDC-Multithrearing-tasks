using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns_Lab1
{
    public abstract class Vehicle
    {
        public abstract string Type { get; init; }
        public abstract string ModelName { get; init; }
        public abstract int Wheels { get; init; }

        public abstract float CargoCapacity { get; init; }

        public abstract int Seats { get; init; }

        public abstract int MaxSpeed { get; init; }


        public abstract string MoveOnSpeed(int speed);

        public abstract string TakePassenger();
        public abstract string DropOffPassenger();



    }
}
