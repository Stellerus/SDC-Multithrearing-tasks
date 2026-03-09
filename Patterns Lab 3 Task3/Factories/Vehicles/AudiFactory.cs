using Patterns_Lab_3_Task3.Cars;
using Patterns_Lab_3_Task3.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Patterns_Lab_3_Task3.Factories.Vehicles
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



}
