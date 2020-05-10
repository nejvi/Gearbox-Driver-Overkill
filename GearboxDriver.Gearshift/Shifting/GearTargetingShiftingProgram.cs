using GearboxDriver.Hardware.ACL;

namespace GearboxDriver.Gearshift
{
    class GearTargetingShiftingProgram : IShiftingProgram
    {
        private GearNumber CurrentGear { get; set; }
        private GearNumber TargetedGear { get; }

        public GearTargetingShiftingProgram(GearNumber targetedGear)
        {
            TargetedGear = targetedGear;
        }

        public SuggestedAction GetSuggestedActionFor(Rpm rpm)
        {
            if (CurrentGear.Value > TargetedGear.Value)
                return SuggestedAction.Downshift;

            if (CurrentGear.Value < TargetedGear.Value)
                return SuggestedAction.Upshift;

            return SuggestedAction.Retain;
        }
    }
}
