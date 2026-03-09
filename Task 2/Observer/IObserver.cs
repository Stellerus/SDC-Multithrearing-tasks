using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_2.Cars;

namespace Task_2.Observer
{
    public interface IObserver
    {
        void OnCarChanged(Car sender, string propertyName, object oldValue, object newValue);
    }
}
