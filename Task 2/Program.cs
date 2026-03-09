using Task_2.Cars;
using Task_2.Factories;
using Task_2.Factories.Cargos;
using Task_2.Factories.Tanks;
using Task_2.Factories.Vehicles;
using Task_2.Observer;

namespace Task_2
{
    class Program
    {
        static void Main()
        {
            //CarFactory factory;

            //factory = new AudiFactory();
            //Car audi = factory.CreateCar();
            //audi.Info();

            //factory = new VolvoFactory();
            //Car volvo = factory.CreateCar();
            //volvo.Info();

            //factory = new AbramsFactory();
            //Car abrams = factory.CreateCar();
            //abrams.Info();


            Console.WriteLine();
            Console.WriteLine("Lab 3 Part3");
            Console.WriteLine();

            Container container = new Container();

            Vehicle audi = new Vehicle
            {
                Weight = 1500,
                Length = 4.5f,
                MaxSpeed = 240
            };

            container.Add(audi);

            audi.Color = "Red";
            audi.Color = "Black";
            audi.WheelDrive = "Front";
            audi.WheelDrive = "All";

        }
    }

}
