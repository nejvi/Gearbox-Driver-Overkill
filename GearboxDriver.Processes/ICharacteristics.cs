using GearboxDriver.Gearshift;
using GearboxDriver.PublishedLanguage.Responsiveness;

namespace GearboxDriver.Processes
{
    public interface ICharacteristics
    {
        public ShiftpointRange GetRangeFor(ResponsivenessMode responsivenessMode, AggressivenessLevel aggressivenessLevel);
    }
}
