using GearboxDriver.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;
using GearboxDriver.Seedwork;

namespace GearboxDriver.Hardware.ACL
{
    public class RPM : ValueObject
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
