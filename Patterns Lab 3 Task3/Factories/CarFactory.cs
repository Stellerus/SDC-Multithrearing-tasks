using Patterns_Lab_3_Task3.Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Patterns_Lab_3_Task3.Factories
{
    public abstract class CarFactory
    {
        public abstract Car CreateCar();
    }

}
