using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsExample
{
    public class Person
    {
        public Gender gender;

        public string name;

        public Person(Gender gender, string name)
        {
            this.gender = gender;
            this.name = name;
        }

        public enum Gender
        {
            Male,
            Female,
            NonBinary
        }
    }

    
}
