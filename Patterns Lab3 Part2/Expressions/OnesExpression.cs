using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns_Lab3_Part2.Expressions
{
    public class OnesExpression : RomanExpression
    {
        public override string One() => "I";
        public override string Four() => "IV";
        public override string Five() => "V";
        public override string Nine() => "IX";
        public override int Multiplier() => 1;
    }
}
