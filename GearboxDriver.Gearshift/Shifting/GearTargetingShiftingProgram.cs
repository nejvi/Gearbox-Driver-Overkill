using GearboxDriver.Hardware.ACL;
using GearboxDriver.PublishedLanguage.Gearbox;

namespace GearboxDriver.Gearshift.Shifting
{
    public class GearTargetingShiftingProgram : IShiftingProgram
    {
        private Gear TargetedGear { get; }

        public GearTargetingShiftingProgram(Gear targetedGear)
        {
            TargetedGear = targetedGear;
        }

        public SuggestedAction GetSuggestedActionFor(Gear currentGear, Rpm rpm)
        {
            if (currentGear.Value > TargetedGear.Value)
                return SuggestedAction.Downshift;

            if (currentGear.Value < TargetedGear.Value)
                return SuggestedAction.Upshift;

            return SuggestedAction.Retain;
        }
    }
}
