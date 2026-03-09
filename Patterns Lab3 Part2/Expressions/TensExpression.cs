using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns_Lab3_Part2.Expressions
{
    public class TensExpression : RomanExpression
    {
        public override string One() => "X";
        public override string Four() => "XL";
        public override string Five() => "L";
        public override string Nine() => "XC";
        public override int Multiplier() => 10;
    }
}
