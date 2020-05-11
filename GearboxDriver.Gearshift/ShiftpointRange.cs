using GearboxDriver.Hardware.ACL;
using GearboxDriver.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace GearboxDriver.Gearshift
{
    public class ShiftpointRange : ValueObject
    {
        public Rpm LowerShiftPoint { get; set; }
        public Rpm UpperShiftPoint { get; set; }

        public ShiftpointRange(Rpm lowerShiftPoint, Rpm upperShiftPoint)
        {
            LowerShiftPoint = lowerShiftPoint;
            UpperShiftPoint = upperShiftPoint;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return UpperShiftPoint.Value - LowerShiftPoint.Value;
        }
    }
}
