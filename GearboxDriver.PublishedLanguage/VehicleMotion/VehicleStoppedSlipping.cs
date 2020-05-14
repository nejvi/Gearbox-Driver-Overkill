using GearboxDriver.Seedwork;

namespace GearboxDriver.PublishedLanguage.VehicleMotion
{
    public class VehicleStoppedSlipping : IEvent
    {
        public override string ToString()
        {
            return $"The vehicle has stopped slipping.";
        }
    }
}
