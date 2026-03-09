using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_2.Cars;

namespace Task_2.Factories.Tanks
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
