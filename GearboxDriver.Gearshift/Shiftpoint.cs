using System.Collections.Generic;
using GearboxDriver.Hardware.ACL;
using GearboxDriver.Seedwork;

namespace GearboxDriver.Gearshift
{
    public class ShiftPoint : ValueObject
    {
        public RPM Threshold { get; }
        public Gear Gear { get; }

        public ShiftPoint(RPM threshold, Gear gear)
        {
            Threshold = threshold;
            Gear = gear;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Threshold;
        }
    }
}
