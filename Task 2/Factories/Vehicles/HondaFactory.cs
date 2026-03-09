using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_2.Cars;

namespace Task_2.Factories.Vehicles
{
    public class HondaFactory : CarFactory
    {
        public override Car CreateCar()
        {
            return new Vehicle
            {
                Weight = 1400,
                Length = 4.3f,
                MaxSpeed = 200
            };
        }
    }

}
