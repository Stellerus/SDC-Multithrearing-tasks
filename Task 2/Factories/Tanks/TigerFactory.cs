using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_2.Cars;

namespace Task_2.Factories.Tanks
{
    public class TigerFactory : CarFactory
    {
        public override Car CreateCar()
        {
            return new Tank
            {
                Weight = 57000,
                Length = 8.5f,
                MaxSpeed = 45
            };
        }
    }
}
