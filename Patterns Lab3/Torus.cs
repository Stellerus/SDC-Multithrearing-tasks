using Patterns_Lab3.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns_Lab3
{
    public class Torus : IShape
    {
        public double R { get; } // расстояние от центра до центра трубы
        public double r { get; } // радиус трубы

        public Torus(double R, double r)
        {
            this.R = R;
            this.r = r;
        }

        public void Accept(IShapeVisitor visitor) => visitor.Visit(this);
    }
}
