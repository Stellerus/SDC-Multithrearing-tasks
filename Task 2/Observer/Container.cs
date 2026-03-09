using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_2.Cars;

namespace Task_2.Observer
{
    public class Container : IObserver
    {
        private List<Car> cars = new List<Car>();

        public void Add(Car car)
        {
            cars.Add(car);

            car.PropertyChanged += OnCarChanged;

            Console.WriteLine($"Added car of type {car.GetType().Name}");
        }

        public void OnCarChanged(Car sender, string propertyName, object oldValue, object newValue)
        {
            Console.WriteLine($"{sender.GetType().Name}: property {propertyName} changed from {oldValue} to {newValue}");
        }
    }
}

