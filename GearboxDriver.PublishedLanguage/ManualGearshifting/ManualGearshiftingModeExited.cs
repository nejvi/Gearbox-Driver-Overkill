using GearboxDriver.Seedwork;

namespace GearboxDriver.PublishedLanguage.ManualGearshifting
{
    public class ManualGearshiftingModeExited : IEvent
    {
        public override string ToString()
        {
            return $"Manual mode exited";
        }
    }
}
