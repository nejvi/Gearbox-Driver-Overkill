using GearboxDriver.Seedwork;

namespace GearboxDriver.PublishedLanguage.Responsiveness
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
