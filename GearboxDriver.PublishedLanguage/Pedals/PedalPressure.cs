using GearboxDriver.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace GearboxDriver.PublishedLanguage.Pedals
{
    public class PedalPressure : ValueObject
    {
        public double Value { get; }

        public PedalPressure(double value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
