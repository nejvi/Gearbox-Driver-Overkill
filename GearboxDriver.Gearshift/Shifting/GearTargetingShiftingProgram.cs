using GearboxDriver.Hardware.ACL;

namespace GearboxDriver.Gearshift
{
    class GearTargetingShiftingProgram : IShiftingProgram
    {
        private Gear CurrentGear { get; set; }
        private Gear TargetedGear { get; }

        public GearTargetingShiftingProgram(Gear targetedGear)
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
