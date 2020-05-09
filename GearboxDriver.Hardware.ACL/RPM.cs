using GearboxDriver.Seedwork;
using System;
using System.Collections.Generic;

namespace GearboxDriver.Hardware.ACL
{
    public class RPM : ValueObject, IComparable<RPM>
    {
        public double Value { get; }

        public RPM(double value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
