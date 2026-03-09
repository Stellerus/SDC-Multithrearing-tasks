using Patterns_Lab_3_Task3.Cars;
using Patterns_Lab_3_Task3.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Patterns_Lab_3_Task3.Factories.Tanks
{
    public class ManFactory : CarFactory
    {
        public override Car CreateCar()
        {
            return new Cargo
            {
                Weight = 8500,
                Length = 8.2f,
                MaxSpeed = 115
            };
        }
    }
}
