using GearboxDriver.Hardware.ACL;

namespace GearboxDriver.Gearshift
{
    class GearTargetingShiftingProgram : IShiftingProgram
    {
        private int CurrentGear { get; set; } // todo value object
        private int TargetedGear { get; } // todo value object

        public GearTargetingShiftingProgram(int targetedGear)
        {
            TargetedGear = targetedGear;
        }

        public SuggestedAction GetSuggestedActionFor(Rpm rpm)
        {
            if (CurrentGear > TargetedGear)
                return SuggestedAction.Downshift;

            if (CurrentGear < TargetedGear)
                return SuggestedAction.Upshift;

            return SuggestedAction.Retain;
        }
    }
}
