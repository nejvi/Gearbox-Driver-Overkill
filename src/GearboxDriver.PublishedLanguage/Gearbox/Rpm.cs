using System.Collections.Generic;
using GearboxDriver.Seedwork;

namespace GearboxDriver.PublishedLanguage.Gearbox
{
    public class Rpm : ValueObject
    {
        public double Value { get; }

        public Rpm(double value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
