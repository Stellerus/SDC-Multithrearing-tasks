using Patterns_Lab3.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns_Lab3
{
    public class Cube : IShape
    {
        public double Side { get; }

        public Cube(double side)
        {
            Side = side;
        }

        public void Accept(IShapeVisitor visitor) => visitor.Visit(this);
    }
}
