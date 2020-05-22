using GearboxDriver.PublishedLanguage.Gearbox;

namespace GearboxDriver.Gearshift.Shifting
{
    public interface IShiftingProgram
    {
        SuggestedAction GetSuggestedActionFor(Gear currentGear, Rpm rpm);
    }
}
