using Patterns_Lab1__second_take_.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns_Lab1__second_take_.Factories.Interfaces
{
    public interface IVehicleFactory
    {
        List<Car> CreateCars();
        List<Cargo> CreateCargos();
        List<Tank> CreateTanks();
    }
}
