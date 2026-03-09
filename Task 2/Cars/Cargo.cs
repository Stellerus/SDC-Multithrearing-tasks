using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2.Cars
{
    public class Cargo : Car
    {
        private float tonnage;
        public float Tonnage
        {
            get => tonnage;
            set
            {
                var old = tonnage;
                tonnage = value;
                RaisePropertyChanged(nameof(Tonnage), old, value);
            }
        }

        private float tankVolume;
        public float TankVolume
        {
            get => tankVolume;
            set
            {
                var old = tankVolume;
                tankVolume = value;
                RaisePropertyChanged(nameof(TankVolume), old, value);
            }
        }

        private int axlesAmount;
        public int AxlesAmount
        {
            get => axlesAmount;
            set
            {
                var old = axlesAmount;
                axlesAmount = value;
                RaisePropertyChanged(nameof(AxlesAmount), old, value);
            }
        }

        public override void Info() => Console.WriteLine("Cargo");
    }
}
