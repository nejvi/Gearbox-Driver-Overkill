using GearboxDriver.Hardware.ACL;

namespace GearboxDriver.Gearshift.Negotiaton
{
    public class FollowRpmDemand
    {
        public Rpm UpperShifpoint { get; }
        public Rpm LowerShiftpoint { get; }

        public FollowRpmDemand(Rpm upperShifpoint, Rpm lowerShiftpoint)
        {
            UpperShifpoint = upperShifpoint;
            LowerShiftpoint = lowerShiftpoint;
        }

        public FollowRpmDemand AsAffectedBy(ModifySmoothnessDemand modificator)
        {
            return new FollowRpmDemand(new Rpm(UpperShifpoint.Value * modificator.Percentage), new Rpm(LowerShiftpoint.Value * modificator.Percentage)); // Todo review
        }
    }
}