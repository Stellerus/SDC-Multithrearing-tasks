using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns_Lab1
{
    public class Car : Vehicle
    {
        private int wheels;
        private float cargoCapacity;
        private int maxSpeed;
        private int seats;

        public override string Type { get; init; } = "Car";
        public override string ModelName { get ; init ; }
        public override int Wheels 
        {
            get => wheels;
            init => wheels = value = Math.Clamp(value,3,4);
        }
        public override float CargoCapacity { get => cargoCapacity; init => cargoCapacity = Math.Clamp(value,0,50); }
        public override int Seats { get => seats; init => seats = value; }
        public override int MaxSpeed { get => maxSpeed; init => maxSpeed = Math.Clamp(value,0,300); }

        public Car(string modelName, int wheels, float cargoCapacity, int seats, int MaxSpeed)
        {
            ModelName = modelName;
            Wheels = wheels;
            CargoCapacity = cargoCapacity;
            Seats = seats;
        }

        public override string DropOffPassenger()
        {
            throw new NotImplementedException();
        }

        public override string MoveOnSpeed(int speed)
        {
            throw new NotImplementedException();
        }

        public override string TakePassenger()
        {
            throw new NotImplementedException();
        }
    }
}
