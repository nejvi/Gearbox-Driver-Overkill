namespace GearboxDriver.Gearshift.Negotiation
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