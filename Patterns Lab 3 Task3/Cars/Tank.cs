using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns_Lab_3_Task3.Cars
{
    public class Tank : Car
    {
        private float projectileCaliber;
        public float ProjectileCaliber
        {
            get => projectileCaliber;
            set
            {
                var old = projectileCaliber;
                projectileCaliber = value;
                RaisePropertyChanged(nameof(ProjectileCaliber), old, value);
            }
        }

        private int shotsPerMinute;
        public int ShotsPerMinute
        {
            get => shotsPerMinute;
            set
            {
                var old = shotsPerMinute;
                shotsPerMinute = value;
                RaisePropertyChanged(nameof(ShotsPerMinute), old, value);
            }
        }

        private int crewSize;
        public int CrewSize
        {
            get => crewSize;
            set
            {
                var old = crewSize;
                crewSize = value;
                RaisePropertyChanged(nameof(CrewSize), old, value);
            }
        }

        public override void Info() => Console.WriteLine("Tank");
    }
}
