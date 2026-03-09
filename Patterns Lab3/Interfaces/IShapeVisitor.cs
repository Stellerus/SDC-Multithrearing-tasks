using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns_Lab3.Interfaces
{
    public interface IShapeVisitor
    {
        void Visit(Sphere sphere);
        void Visit(Parallelepiped parallelepiped);
        void Visit(Torus torus);
        void Visit(Cube cube);
    }
}
