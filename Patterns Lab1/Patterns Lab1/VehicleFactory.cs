using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Patterns_Lab1
{
    public abstract class VehicleFactory
    {
        List<Vehicle> AllCreatedVehicles {  get; set; }

        public VehicleFactory()
        {
            AllCreatedVehicles = new List<Vehicle>();
        }



    }
}
