using GearboxDriver.Gearshift;
using GearboxDriver.PublishedLanguage.Responsiveness;

namespace GearboxDriver.Processes
{
    public interface IEngineCharacteristics
    {
        public ShiftpointRange GetRangeFor(ResponsivenessMode responsivenessMode, AggressivenessLevel aggressivenessLevel);
    }
}
