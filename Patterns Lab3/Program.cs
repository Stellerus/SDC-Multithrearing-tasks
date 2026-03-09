using Patterns_Lab3;
using Patterns_Lab3.Interfaces;


IShape[] shapes =
{
    new Sphere(3),
    new Parallelepiped(2, 3, 4),
    new Torus(5, 1),
    new Cube(2)
};

foreach (var shape in shapes)
{
    var visitor = new VolumeVisitor();
    shape.Accept(visitor);
    Console.WriteLine($"Volume: {visitor.Volume}");
}


