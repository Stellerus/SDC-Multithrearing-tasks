using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_2.Cars;

namespace Task_2.Factories.Cargos
{
    public class VolvoFactory : CarFactory
    {
        public override Car CreateCar()
        {
            return new Cargo
            {
                Weight = 8000,
                Length = 8.0f,
                MaxSpeed = 120
            };
        }
    }
}
