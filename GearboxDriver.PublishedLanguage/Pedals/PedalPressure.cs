using GearboxDriver.Seedwork;
using System.Collections.Generic;

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
