using Patterns_Lab3.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns_Lab3
{
    public class VolumeVisitor : IShapeVisitor
    {
        public double Volume { get; private set; }

        public void Visit(Sphere sphere)
        {
            Volume = 4.0 / 3.0 * Math.PI * Math.Pow(sphere.Radius, 3);
        }

        public void Visit(Parallelepiped p)
        {
            Volume = p.A * p.B * p.C;
        }

        public void Visit(Torus t)
        {
            Volume = 2 * Math.PI * Math.PI * t.R * Math.Pow(t.r, 2);
        }

        public void Visit(Cube cube)
        {
            Volume = Math.Pow(cube.Side, 3);
        }
    }
}
