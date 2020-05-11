using GearboxDriver.Hardware.ACL;

namespace GearboxDriver.Gearshift.Shifting
{
    public class YieldingShiftingProgram : IShiftingProgram
    {
        public SuggestedAction GetSuggestedActionFor(Rpm rpm)
        {
            return SuggestedAction.Retain;
        }
    }
}
