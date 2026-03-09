using Patterns_Lab_3_Task3.Cars;
using Patterns_Lab_3_Task3.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Patterns_Lab_3_Task3.Factories.Tanks
{
    public class AbramsFactory : CarFactory
    {
        public override Car CreateCar()
        {
            return new Tank
            {
                Weight = 62000,
                Length = 9.7f,
                MaxSpeed = 67
            };
        }
    }
}
