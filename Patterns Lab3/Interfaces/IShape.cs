using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns_Lab3.Interfaces
{
    public interface IShape
    {
        void Accept(IShapeVisitor visitor);
    }
}
