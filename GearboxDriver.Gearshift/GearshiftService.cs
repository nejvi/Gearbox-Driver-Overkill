﻿using GearboxDriver.Gearshift.Negotiaton;
using GearboxDriver.Hardware.ACL;

namespace GearboxDriver.Gearshift
{
    public class GearshiftService
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

        public void TargetGear(GearNumber gearNumber)
        {
            _negotiator.Issue(new TargetGearDemand(gearNumber));

            _gearshifter.SetProgram(_negotiator.Negotiate());
        }

        public void StopTargetingGear()
        {
            _negotiator.RevokeTargetGearDemand();

            _gearshifter.SetProgram(_negotiator.Negotiate());
        }

        public void KeepFollowingRpm(Rpm lowerShiftpoint, Rpm upperShiftpoint) // TODO Value Object - range
        {
            _negotiator.Issue(new FollowRpmDemand(lowerShiftpoint, upperShiftpoint));

            _gearshifter.SetProgram(_negotiator.Negotiate());
        }

        public void ApplySharpnessFactor(double percentage) // TODO Value Object - range
        {
            _negotiator.Issue(new ModifySmoothnessDemand(percentage)); // TODO may not be needed!

            _gearshifter.SetProgram(_negotiator.Negotiate());
        }
    }
}