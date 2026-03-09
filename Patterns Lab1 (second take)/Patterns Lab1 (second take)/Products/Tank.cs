using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns_Lab1__second_take_.Products
{
    public class Tank : Vehicle
    {
        public virtual double ProjectileCaliber { get; set; }
        public virtual int ShotsPerMinute { get; set; }
        public virtual int CrewSize { get; set; }

        public override string ToString()
        {
            return base.ToString() +
                   $", Caliber: {ProjectileCaliber}, RPM: {ShotsPerMinute}, Crew: {CrewSize}";
        }
    }
}
