using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns_Lab3_Part2.Expressions
{
    public class HundredsExpression : RomanExpression
    {
        public override string One() => "C";
        public override string Four() => "CD";
        public override string Five() => "D";
        public override string Nine() => "CM";
        public override int Multiplier() => 100;
    }
}
