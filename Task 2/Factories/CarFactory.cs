using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Task_2.Cars;

namespace Task_2.Factories
{
    public abstract class CarFactory
    {
        public abstract Car CreateCar();
    }

}
