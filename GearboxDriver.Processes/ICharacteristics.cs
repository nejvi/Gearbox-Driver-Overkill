using GearboxDriver.Cabin.Responsiveness;
using GearboxDriver.Gearshift;

namespace GearboxDriver.Processes
{
    public interface ICharacteristics
    {
        public ShiftpointRange GetRangeFor(ResponsivenessMode responsivenessMode, AggressivenessLevel aggressivenessLevel);
    }
}
