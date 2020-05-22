using GearboxDriver.Seedwork;

namespace GearboxDriver.PublishedLanguage.ManualGearshifting
{
    public class ManualGearshiftingModeEntered : IEvent
    {
        public override string ToString()
        {
            return $"Manual mode has been entered";
        }
    }
}
