using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns_Laba1.task_1
{
    public class TuplaFactory : SweetsFactory
    {
        public override Sweet CreateSweet()
        {
            return new Tupla();
        }
    }
}
