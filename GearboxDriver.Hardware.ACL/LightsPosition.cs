using System;
using System.Collections.Generic;
using System.Text;

namespace GearboxDriver.Hardware.ACL
{
    public class LightsPosition : ValueObject
    {
        public int Value { get; }

        public LightsPosition(int value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
