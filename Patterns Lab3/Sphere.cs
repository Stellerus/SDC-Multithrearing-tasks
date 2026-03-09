using Patterns_Lab3.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns_Lab3
{
    public class Sphere : IShape
    {
        public double Radius { get; }

        public Sphere(double radius)
        {
            Radius = radius;
        }

        public void Accept(IShapeVisitor visitor) => visitor.Visit(this);
    }
}
