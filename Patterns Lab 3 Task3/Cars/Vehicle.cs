using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns_Lab_3_Task3.Cars
{
    public class Vehicle : Car
    {
        private string wheelDrive;
        public string WheelDrive
        {
            get => wheelDrive;
            set
            {
                var old = wheelDrive;
                wheelDrive = value;
                RaisePropertyChanged(nameof(WheelDrive), old, value);
            }
        }

        private string carClass;
        public string Class
        {
            get => carClass;
            set
            {
                var old = carClass;
                carClass = value;
                RaisePropertyChanged(nameof(Class), old, value);
            }
        }

        private string color;
        public string Color
        {
            get => color;
            set
            {
                var old = color;
                color = value;
                RaisePropertyChanged(nameof(Color), old, value);
            }
        }

        public override void Info() => Console.WriteLine("Vehicle");
    }
}
