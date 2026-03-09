using Patterns_Lab_3_Task3.Cars;
using Patterns_Lab_3_Task3.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Patterns_Lab_3_Task3.Factories.Cargos
{
    public class ScaniaFactory : CarFactory
    {
        public override Car CreateCar()
        {
            return new Cargo
            {
                Weight = 9000,
                Length = 8.5f,
                MaxSpeed = 110
            };
        }
    }
}
