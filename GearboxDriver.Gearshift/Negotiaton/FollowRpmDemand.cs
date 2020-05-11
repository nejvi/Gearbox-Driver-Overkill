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

        public FollowRpmDemand AsAffectedBy(ModifySmoothnessDemand modificator)
        {
            return new FollowRpmDemand( new ShiftpointRange(
                new Rpm(ShiftpointRange.LowerShiftPoint.Value * modificator.Percentage), 
                new Rpm(ShiftpointRange.UpperShiftPoint.Value * modificator.Percentage)
                )); // Todo review
        }
    }
}