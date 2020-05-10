using GearboxDriver.Seedwork;

namespace GearboxDriver.Gearshift.Negotiator
{
    public class ProgramNegotiator
    {
        private Optional<YieldDemand> _yield;
        private Optional<TargetGearDemand> _targetGear;
        private Optional<ModifySmoothnessDemand> _modifySmoothness;
        private Optional<FollowRpmDemand> _followRpm;

        public ProgramNegotiator()
        {
        }

        public IShiftingProgram Negotiate()
        {
            if (_yield.HasValue)
                return new YieldingShiftingProgram();

            if (_targetGear.HasValue)
                return new GearTargetingShiftingProgram(_targetGear.InnerValue.Gear);

            if (_modifySmoothness.HasValue && _followRpm.HasValue)
            {
                var demand = _followRpm.InnerValue.AsAffectedBy(_modifySmoothness.InnerValue);

                return new RpmBasedShiftingProgram(demand.LowerShiftpoint, demand.UpperShifpoint);
            }
            
            if (_followRpm.HasValue)
                return new RpmBasedShiftingProgram(_followRpm.InnerValue.LowerShiftpoint, _followRpm.InnerValue.UpperShifpoint);

            return new YieldingShiftingProgram();
        }

        public void Issue(YieldDemand demand)
        {
            if (_yield.HasValue)
                throw new DomainRuleViolatedException("Yield demand has already been issued.");
        }

        public void Revoke(YieldDemand demand)
        {
            if (!_yield.HasValue)
                throw new DomainRuleViolatedException("Yield demand has not been been issued.");

            _yield = new Optional<YieldDemand>(demand);
        }

        public void Issue(TargetGearDemand demand)
        {
            if (_targetGear.HasValue)
                throw new DomainRuleViolatedException("Target gear demand has already been issued.");
        }

        public void Revoke(TargetGearDemand demand)
        {
            if (!_targetGear.HasValue)
                throw new DomainRuleViolatedException("Target gear demand has not been been issued.");

            _targetGear = new Optional<TargetGearDemand>(demand);
        }

        public void Issue(ModifySmoothnessDemand demand)
        {
            if (_modifySmoothness.HasValue)
                throw new DomainRuleViolatedException("Modify smoothness demand has already been issued.");
        }

        public void Revoke(ModifySmoothnessDemand demand)
        {
            if (!_modifySmoothness.HasValue)
                throw new DomainRuleViolatedException("Modify smoothness demand has not been been issued.");

            _modifySmoothness = new Optional<ModifySmoothnessDemand>(demand);
        }

        public void Issue(FollowRpmDemand demand)
        {
            if (_followRpm.HasValue)
                throw new DomainRuleViolatedException("Follow RPM demand has already been issued.");
        }

        public void Revoke(FollowRpmDemand demand)
        {
            if (!_followRpm.HasValue)
                throw new DomainRuleViolatedException("Follow RPM demand has not been been issued.");

            _followRpm = new Optional<FollowRpmDemand>(demand);
        }
    }
}
