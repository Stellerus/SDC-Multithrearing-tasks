using Patterns_Lab1__second_take_.Factories.Interfaces;
using Patterns_Lab1__second_take_.Products;
using Patterns_Lab1__second_take_.Products.Cargos;
using Patterns_Lab1__second_take_.Products.Cars;
using Patterns_Lab1__second_take_.Products.Tanks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns_Lab1__second_take_.Factories
{
    public class Factory : IVehicleFactory
    {

        public List<Car> CreateCars()
        {
            return new List<Car>() { new Audi(), new Honda(), new Tesla()};
        }

        public List<Cargo> CreateCargos()
        {
            return new List<Cargo> { new Man(), new Scania(), new Volvo()};
        }

        public List<Tank> CreateTanks()
        {
            return new List<Tank> { new Abrams(), new Merkava(), new Tiger() };
        }


    }
}
