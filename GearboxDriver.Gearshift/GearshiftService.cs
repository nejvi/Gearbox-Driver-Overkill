using GearboxDriver.Gearshift.Negotiaton;
using GearboxDriver.Hardware.ACL;

namespace GearboxDriver.Gearshift
{
    public class GearshiftService : IGearshiftService
    {
        private readonly Negotiator _negotiator;
        private readonly AutomaticGearshifter _gearshifter;

        public GearshiftService(Negotiator negotiator, AutomaticGearshifter gearshifter)
        {
            _negotiator = negotiator;
            _gearshifter = gearshifter;
        }

        public void AbstainFromChangingGears()
        {
            _negotiator.Issue(new YieldDemand());

            _gearshifter.SetProgram(_negotiator.Negotiate());
        }

        public void StopAbstainingFromChangingGears()
        {
            _negotiator.RevokeYieldDemand();

            _gearshifter.SetProgram(_negotiator.Negotiate());
        }

        public void TargetGear(Gear gearNumber)
        {
            _negotiator.Issue(new TargetGearDemand(gearNumber));

            _gearshifter.SetProgram(_negotiator.Negotiate());
        }

        public void StopTargetingGear()
        {
            _negotiator.RevokeTargetGearDemand();

            _gearshifter.SetProgram(_negotiator.Negotiate());
        }

        public void KeepFollowingRpm(ShiftpointRange shiftpointRange)
        {
            _negotiator.Issue(new FollowRpmDemand(shiftpointRange));

            _gearshifter.SetProgram(_negotiator.Negotiate());
        }

        public void ApplySharpnessFactor(Percentage percentage)
        {
            _negotiator.Issue(new ModifySmoothnessDemand(percentage)); // TODO may not be needed!

            _gearshifter.SetProgram(_negotiator.Negotiate());
        }
    }
}
