using GearboxDriver.PublishedLanguage;
using GearboxDriver.PublishedLanguage.Gearbox;
using GearboxDriver.PublishedLanguage.Pedals;
using GearboxDriver.PublishedLanguage.Responsiveness;

namespace GearboxDriver.Processes
{
    public interface IKickdownCharacteristics
    {
        SuggestedKickdownAction GetActionFor(ResponsivenessMode responsivenessMode, PedalPressure pedalPressure, Rpm currentRpm);
    }
}
