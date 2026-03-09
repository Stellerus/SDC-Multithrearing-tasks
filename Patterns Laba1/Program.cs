using Patterns_Laba1.task_1;

namespace Patterns_Laba1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SweetsFactory factory1;
            SweetsFactory factory2;

            factory1 = new SnickersFactory();
            factory2 = new TuplaFactory();

            Sweet snickers = factory1.CreateSweet();
            Sweet tupla = factory2.CreateSweet();
        }
    }
}
