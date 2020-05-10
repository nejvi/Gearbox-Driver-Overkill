using GearboxDriver.Hardware.ACL;
using GearboxDriver.Seedwork;

namespace GearboxDriver.Gearshift
{
    public class RpmBasedShiftingProgram : IShiftingProgram
    {
        private Rpm LowerShiftPoint { get; }
        private Rpm UpperShiftPoint { get; }

        public RpmBasedShiftingProgram(Rpm lowerShiftPoint, Rpm upperShiftPoint)
        {
            if (lowerShiftPoint.Value <= 0)
                throw new DomainRuleViolatedException("Lower shift point cannot be less or equal to zero");

            if (upperShiftPoint.Value <= lowerShiftPoint.Value)
                throw new DomainRuleViolatedException("Upper shift point cannot be lower or equal to the lower shift point");

            LowerShiftPoint = lowerShiftPoint;
            UpperShiftPoint = upperShiftPoint;
        }


        public SuggestedAction GetSuggestedActionFor(Rpm rpm)
        {
            if (rpm.Value < LowerShiftPoint.Value) return SuggestedAction.Downshift;

            if (rpm.Value > UpperShiftPoint.Value) return SuggestedAction.Upshift;

            return SuggestedAction.Retain;
        } 
    }
}
