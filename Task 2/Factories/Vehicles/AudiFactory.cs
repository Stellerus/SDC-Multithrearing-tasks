using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_2.Cars;

namespace Task_2.Factories.Vehicles
{
    public class AudiFactory : CarFactory
    {
        public override Car CreateCar()
        {
            return new Vehicle
            {
                Weight = 1500,
                Length = 4.5f,
                MaxSpeed = 240
            };
        }
    }

    public class TeslaFactory : CarFactory
    {
        public override Car CreateCar()
        {
            return new Vehicle
            {
                Weight = 1800,
                Length = 4.7f,
                MaxSpeed = 250
            };
        }
    }


}
