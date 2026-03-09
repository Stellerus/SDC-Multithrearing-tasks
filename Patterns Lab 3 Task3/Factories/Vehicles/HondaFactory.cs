using Patterns_Lab_3_Task3.Cars;
using Patterns_Lab_3_Task3.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Patterns_Lab_3_Task3.Factories.Vehicles
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
