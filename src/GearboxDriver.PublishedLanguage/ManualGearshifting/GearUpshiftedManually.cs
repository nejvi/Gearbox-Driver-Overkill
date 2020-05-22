using GearboxDriver.Seedwork;

namespace GearboxDriver.PublishedLanguage.ManualGearshifting
{
    public class GearUpshiftedManually : IEvent
    {
        public override string ToString()
        {
            return $"Gear upshifted manually.";
        }
    }
}
