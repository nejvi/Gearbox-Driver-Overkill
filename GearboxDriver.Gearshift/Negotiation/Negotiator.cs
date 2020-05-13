using GearboxDriver.Gearshift.Shifting;
using GearboxDriver.Seedwork;

namespace GearboxDriver.Gearshift.Negotiation
{
    public class Negotiator
    {
        private Optional<YieldDemand> _yield;
        private Optional<TargetGearDemand> _targetGear;
        private Optional<EngineBrakingDemand> _performEngineBraking;
        private Optional<FollowRpmDemand> _followRpm;

        public Negotiator()
        {
            _yield = Optional<YieldDemand>.Empty();
            _targetGear = Optional<TargetGearDemand>.Empty();
            _performEngineBraking = Optional<EngineBrakingDemand>.Empty();
            _followRpm = Optional<FollowRpmDemand>.Empty();
        }

        public IShiftingProgram Negotiate()
        {
            if (_yield.HasValue)
                return new YieldingShiftingProgram();

            if (_targetGear.HasValue)
                return new GearTargetingShiftingProgram(_targetGear.InnerValue.Gear);

            if (_followRpm.HasValue)
            {
                if (_performEngineBraking.HasValue)
                    return new EngineBrakingShiftingProgram(new RpmBasedShiftingProgram(_followRpm.InnerValue.ShiftpointRange));

                return new RpmBasedShiftingProgram(_followRpm.InnerValue.ShiftpointRange);
            }
                

            return new YieldingShiftingProgram();
        }

        public void Issue(YieldDemand demand)
        {
            _yield = new Optional<YieldDemand>(demand);
        }

        public void RevokeYieldDemand()
        {
            if (!_yield.HasValue)
                throw new DomainRuleViolatedException("Yield demand has not been been issued.");

            _yield = Optional<YieldDemand>.Empty();
        }

        public void Issue(TargetGearDemand demand)
        {
            _targetGear = new Optional<TargetGearDemand>(demand);
        }

        public void RevokeTargetGearDemand()
        {
            if (!_targetGear.HasValue)
                throw new DomainRuleViolatedException("Target gear demand has not been been issued.");

            _targetGear = Optional<TargetGearDemand>.Empty();
        }

        public void Issue(FollowRpmDemand demand)
        {
            _followRpm = new Optional<FollowRpmDemand>(demand);
        }

        public void Issue(EngineBrakingDemand demand)
        {
            _performEngineBraking = new Optional<EngineBrakingDemand>(demand);
        }

        public void RevokeEngineBrakingDemand()
        {
            if (!_performEngineBraking.HasValue)
                throw new DomainRuleViolatedException("Engine braking demand has not been been issued.");

            _performEngineBraking = Optional<EngineBrakingDemand>.Empty();
        }
    }
}
