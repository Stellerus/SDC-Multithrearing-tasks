using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns_Lab1__second_take_.Products
{
    public class Cargo : Vehicle
    {
        public virtual double Tonnage { get; set; }
        public virtual double TankVolume { get; set; }
        public virtual int AxlesAmount { get; set; }

        public override string ToString()
        {
            return base.ToString() +
                   $", Tonnage: {Tonnage}, TankVolume: {TankVolume}, AxlesAmount: {AxlesAmount}";
        }
    }
}
