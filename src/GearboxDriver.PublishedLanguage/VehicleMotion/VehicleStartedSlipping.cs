using GearboxDriver.Seedwork;

namespace GearboxDriver.PublishedLanguage.VehicleMotion
{
    public class VehicleStartedSlipping : IEvent
    {
        public override string ToString()
        {
            return $"The vehicle has started slipping.";
        }
    }
}
