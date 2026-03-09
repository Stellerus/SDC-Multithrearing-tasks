using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_2.Cars;

namespace Task_2.Factories.Cargos
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
