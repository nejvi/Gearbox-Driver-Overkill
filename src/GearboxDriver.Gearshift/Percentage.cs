using GearboxDriver.Seedwork;
using System.Collections.Generic;

namespace GearboxDriver.Gearshift
{
    public class Percentage : ValueObject
    {
        public double Value { get; }

        public Percentage(double value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
