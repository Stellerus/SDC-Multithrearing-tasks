using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_2.Observer;

namespace Task_2.Cars
{
    public abstract class Car
    {
        public event Action<Car, string, object, object> PropertyChanged;

        protected void RaisePropertyChanged(string propertyName, object oldValue, object newValue)
        {
            PropertyChanged?.Invoke(this, propertyName, oldValue, newValue);
        }

        public float Weight
        {
            get => weight;
            set
            {
                var old = weight;
                weight = value;
                RaisePropertyChanged(nameof(Weight), old, value);
            }
        }
        private float weight;

        public float Length
        {
            get => length;
            set
            {
                var old = length;
                length = value;
                RaisePropertyChanged(nameof(Length), old, value);
            }
        }
        private float length;

        public float MaxSpeed
        {
            get => maxSpeed;
            set
            {
                var old = maxSpeed;
                maxSpeed = value;
                RaisePropertyChanged(nameof(MaxSpeed), old, value);
            }
        }
        private float maxSpeed;

        public abstract void Info();
    }
}
