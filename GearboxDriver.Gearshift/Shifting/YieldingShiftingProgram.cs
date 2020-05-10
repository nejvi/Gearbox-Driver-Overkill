using GearboxDriver.Hardware.ACL;

namespace GearboxDriver.Gearshift
{
    class YieldingShiftingProgram : IShiftingProgram
    {
        public SuggestedAction GetSuggestedActionFor(Rpm rpm)
        {
            return SuggestedAction.Retain;
        }
    }
}
