using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CarsExample
{
    public class Car
    {
        public int seats;
        public int wheels;

        List<Person> people = new List<Person>();

        public Car(int seats, int wheels)
        {
            this.seats = seats;
            this.wheels = wheels;
        }

        public void Drive()
        {
            Console.WriteLine("Drive");
        }

        public void Stop()
        {
            Console.WriteLine("Stop");
        }

        public void Sex(Person person1, Person person2)
        {
            Console.WriteLine("UwU");
        }

        public void Sex()
        {
            Console.WriteLine($"{people[0].name} and {people[1].name} have sex. UwU");
        }

        public void AddPerson(Person person)
        {
            people.Add(person);
        }
    }
}
