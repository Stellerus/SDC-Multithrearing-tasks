using Patterns_Lab3.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns_Lab3
{
    public class Parallelepiped : IShape
    {
        public double A { get; }
        public double B { get; }
        public double C { get; }

        public Parallelepiped(double a, double b, double c)
        {
            A = a; B = b; C = c;
        }

        public void Accept(IShapeVisitor visitor) => visitor.Visit(this);
    }
}
