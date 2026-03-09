using Patterns_Lab1__second_take_.Factories;
using Patterns_Lab1__second_take_.Factories.Interfaces;



void FactoryTest(IVehicleFactory factory)
{
    Progam();

    Console.WriteLine("\nCargo");
    var cargo = factory.CreateCargo();
    Console.WriteLine(cargo.ToString()); ;

    Console.WriteLine("\nTank");
    var tank = factory.CreateTank();
    Console.WriteLine(tank.ToString()); ;


    Console.WriteLine($"\n---Completed factory test---");
}















void Progam()
{
    Console.WriteLine("Car");
    var vehicle = factory.CreateCar();
    Console.WriteLine(vehicle.ToString()); ;
}