using GearboxDriver.PublishedLanguage.Gearbox;

namespace GearboxDriver.Gearshift.Shifting
{
    public class YieldingShiftingProgram : IShiftingProgram
    {
        public SuggestedAction GetSuggestedActionFor(Gear currentGear, Rpm rpm)
        {
            return SuggestedAction.Retain;
        }
    }
}
