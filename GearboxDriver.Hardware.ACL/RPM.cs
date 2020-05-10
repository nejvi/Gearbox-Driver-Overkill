using GearboxDriver.Seedwork;
using System;
using System.Collections.Generic;

namespace GearboxDriver.Hardware.ACL
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
