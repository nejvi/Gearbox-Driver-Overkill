using GearboxDriver.Seedwork;
using System.Collections.Generic;
using GearboxDriver.PublishedLanguage.Gearbox;

namespace GearboxDriver.Gearshift
{
    public class ShiftpointRange : ValueObject
    {
        public Rpm LowerShiftPoint { get; set; }
        public Rpm UpperShiftPoint { get; set; }

        public ShiftpointRange(Rpm lowerShiftPoint, Rpm upperShiftPoint)
        {
            if (lowerShiftPoint.Value > upperShiftPoint.Value)
                throw new DomainRuleViolatedException("Upper shiftpoint value cannot be lower than lower shiftpoint value");

            LowerShiftPoint = lowerShiftPoint;
            UpperShiftPoint = upperShiftPoint;
        }

        public ShiftpointRange AsModifiedBy(Percentage percentage)
        {
            return new ShiftpointRange(new Rpm(LowerShiftPoint.Value * percentage.Value), new Rpm(UpperShiftPoint.Value * percentage.Value));
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return UpperShiftPoint.Value;
            yield return LowerShiftPoint.Value;
        }
    }
}
