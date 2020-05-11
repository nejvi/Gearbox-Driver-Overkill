using GearboxDriver.Hardware.ACL;

namespace GearboxDriver.Gearshift.Negotiaton
{
    public class FollowRpmDemand
    {
        public ShiftpointRange ShiftpointRange { get; }

        public FollowRpmDemand(ShiftpointRange shiftpointRange)
        {
            ShiftpointRange = shiftpointRange;
        }
    }
}