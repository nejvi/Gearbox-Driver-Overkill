using GearboxDriver.Hardware.ACL;
using GearboxDriver.Seedwork;

namespace GearboxDriver.Processes
{
    public class SmoothBrakingWithTrailerAttached : IProcessManager
    {
        private bool CarMovingDownhill { get; set; }

        public SmoothBrakingWithTrailerAttached()
        {
            CarMovingDownhill = false;
        }

        public void ApplyEvent(IEvent @event)
        {
            switch (@event)
            {
                case VehicleTiltedDownhill _:
                    if (!CarMovingDownhill)
                    {
                        CarMovingDownhill = true;
                        // todo downshift ??
                    }
                    
                    break;
                case VehicleTiledToStraightPosition _:
                case VehicleTiltedUphill _:
                    CarMovingDownhill = false;
                    break;
            }
        }
    }
}
