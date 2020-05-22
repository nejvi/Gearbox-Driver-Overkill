using GearboxDriver.Seedwork;

namespace GearboxDriver.PublishedLanguage.ManualGearshifting
{
    public class GearDownshiftedManually : IEvent
    {
        public override string ToString()
        {
            return $"Gear downshifted manually.";
        }
    }
}
