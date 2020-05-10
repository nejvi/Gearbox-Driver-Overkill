using GearboxDriver.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace GearboxDriver.Cabin.Pedals
{
    public class PedalPressure : ValueObject
    {
        public double Pressure { get; }

        public PedalPressure(double pressure)
        {
            Pressure = pressure;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Pressure;
        }
    }
}
