// See https://aka.ms/new-console-template for more information

using CarsExample;

Car smallCar = new Car(4, 4);
smallCar.wheels = 3;
smallCar.seats = 5;

Person person = new Person(Person.Gender.Male, "Poopster");
Person person2 = new Person(Person.Gender.Male, "Sussybakus");

smallCar.AddPerson(person);
smallCar.AddPerson(person2);

smallCar.Sex();