using GearboxDriver.Seedwork;

namespace GearboxDriver.PublishedLanguage.Responsiveness
{
    public class AggressivenessLevelSelected : IEvent
    {
        public AggressivenessLevel Level { get; }

        public AggressivenessLevelSelected(AggressivenessLevel level)
        {
            Level = level;
        }

        public override string ToString()
        {
            return $"Aggressiveness level changed to {Level}.";
        }
    }
}
