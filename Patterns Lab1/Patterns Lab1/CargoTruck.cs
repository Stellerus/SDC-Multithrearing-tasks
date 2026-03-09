using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns_Lab1
{
    internal class CargoTruck : Vehicle
    {
        private int wheels;
        private float cargoCapacity;
        private int seats;
        private int maxSpeed;

        public override string Type { get ; init ; }
        public override string ModelName { get ; init ; }
        public override int Wheels { get => wheels; init => wheels = Math.Clamp(value, 4,6); }
        public override float CargoCapacity { get => cargoCapacity; init => cargoCapacity = value; }
        public float TrailerCargoCapacity { get; set; }
        public bool Trailer {  get; set; }
        public override int Seats { get => seats; init => seats = value; }
        public override int MaxSpeed { get => maxSpeed; init => maxSpeed = value; }


        public CargoTruck() : base()
        {
            
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
