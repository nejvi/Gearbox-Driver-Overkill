using GearboxDriver.Seedwork;

namespace GearboxDriver.Dashboard.Responsiveness
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
