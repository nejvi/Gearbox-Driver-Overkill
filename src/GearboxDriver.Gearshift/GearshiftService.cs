using GearboxDriver.Gearshift.Negotiation;
using GearboxDriver.Gearshift.Shifting;
using GearboxDriver.PublishedLanguage.Gearbox;

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

        public void StopTargetingAnyGear()
        {
            _negotiator.RevokeTargetGearDemand();

            _gearshifter.SetProgram(_negotiator.Negotiate());
        }

        public void KeepFollowingRpm(ShiftpointRange range)
        {
            _negotiator.Issue(new FollowRpmDemand(range));

            _gearshifter.SetProgram(_negotiator.Negotiate());
        }

        public void DoEngineBraking()
        {
            _negotiator.Issue(new EngineBrakingDemand());

            _gearshifter.SetProgram(_negotiator.Negotiate());
        }

        public void StopDoingEngineBraking()
        {
            _negotiator.RevokeEngineBrakingDemand();

            _gearshifter.SetProgram(_negotiator.Negotiate());
        }
    }
}
