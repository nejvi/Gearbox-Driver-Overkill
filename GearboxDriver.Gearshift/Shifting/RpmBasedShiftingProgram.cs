using GearboxDriver.Hardware.ACL;

namespace GearboxDriver.Gearshift.Shifting
{
    public class RpmBasedShiftingProgram : IShiftingProgram
    {
        private ShiftpointRange Range { get; }

        public RpmBasedShiftingProgram(ShiftpointRange range)
        {
            Range = range;
        }

        public SuggestedAction GetSuggestedActionFor(Rpm rpm)
        {
            if (rpm.Value < Range.LowerShiftPoint.Value) return SuggestedAction.Downshift;

            if (rpm.Value > Range.UpperShiftPoint.Value) return SuggestedAction.Upshift;

            return SuggestedAction.Retain;
        } 
    }
}
