using GearboxDriver.Seedwork;

namespace GearboxDriver.Cabin.Responsiveness
{
    public class AggressivenessLevelSelected : IEvent
    {
        public AggressivenessLevelSelected(AggressivenessLevel level)
        {
            Level = level;
        }

        public AggressivenessLevel Level { get; }
    }
}
