using GearboxDriver.Hardware.ACL.LightPositionReporting;
using GearboxDriver.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace GearboxDriver.Hardware.ACL
{
    public class VehicleTiltPosition : ValueObject
    {
        public TiltPosition Value { get; }

        public VehicleTiltPosition(TiltPosition value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
