using System.Collections.Generic;
using GearboxDriver.Cabin.Responsiveness;
using GearboxDriver.Gearshift;
using GearboxDriver.Hardware.ACL;
using GearboxDriver.Seedwork;

namespace GearboxDriver.Processes
{
    public class RpmBasedByModes : IProcessManager
    {
        private readonly IGearshiftService _service;
        private ResponsivenessMode ResponsivenessMode { get; set; }
        private AggressivenessLevel AggressivenessLevel { get; set; }

        private readonly Dictionary<ResponsivenessMode, ShiftpointRange> RangeForModes = new Dictionary<ResponsivenessMode, ShiftpointRange>
        {
            {ResponsivenessMode.Economic, new ShiftpointRange(new Rpm(1000d), new Rpm(2000d)) }
        };

        private readonly Dictionary<AggressivenessLevel, Percentage> PercentageForAggressivenesLevel =
            new Dictionary<AggressivenessLevel, Percentage>
            {
                {AggressivenessLevel.First, new Percentage(1.0d) }
            };

        public RpmBasedByModes(IGearshiftService service)
        {
            _service = service;
        }

        public void ApplyEvent(IEvent @event)
        {
            switch (@event)
            {
                case ComfortModeEntered _:
                    ResponsivenessMode = ResponsivenessMode.Comfort;
                    break;
                case EconomicModeEntered _:
                    ResponsivenessMode = ResponsivenessMode.Economic;
                    break;
                case SportModeEntered _:
                    ResponsivenessMode = ResponsivenessMode.Sport;
                    break;
                case AggressivenessLevelSelected aggressivenessLevel:
                    AggressivenessLevel = aggressivenessLevel.Level;
                    break;
            }

            UpdateCurrentMode();
        }

        public void RpmBasedByMode(double modificator = 1)
        {
            switch (ResponsivenessMode)
            {
                case ResponsivenessMode.Economic:
                    RpmBasedByEconomicMode(modificator);
                    break;
                case ResponsivenessMode.Comfort:
                    RpmBasedByComfortMode(modificator);
                    break;
                case ResponsivenessMode.Sport:
                    RpmBasedBySportMode(modificator);
                    break;
            }
        }

        public void RpmBasedByAggressivenessLevel(AggressivenessLevel aggressivenessLevel)
        {
            AggressivenessLevel = aggressivenessLevel;

            switch (AggressivenessLevel)
            {
                case AggressivenessLevel.First:
                    RpmBasedByMode();
                    break;
                case AggressivenessLevel.Second:
                    RpmBasedByMode(1.3d);
                    break;
                case AggressivenessLevel.Third:
                    RpmBasedByMode(1.3d);
                    break;
            }
        }

        public void RpmBasedByEconomicMode(Percentage percentage)
        {
            ResponsivenessMode = ResponsivenessMode.Economic;

            _service.KeepFollowingRpm(RangeForModes[ResponsivenessMode].AsModifiedBy(PercentageForAggressivenesLevel[AggressivenessLevel]));
        }

        public void RpmBasedByComfortMode(double modificator = 1)
        {
            ResponsivenessMode = ResponsivenessMode.Comfort;

            var lowerShiftPoint = new Rpm(1000d * modificator);
            var upperShiftPoint = new Rpm(2500d * modificator);

            _service.KeepFollowingRpm(new ShiftpointRange(lowerShiftPoint, upperShiftPoint));
        }

        public void RpmBasedBySportMode(double modificator = 1)
        {
            ResponsivenessMode = ResponsivenessMode.Sport;

            var lowerShiftPoint = new Rpm(1500d * modificator);
            var upperShiftPoint = new Rpm(2500d * modificator);

            _service.KeepFollowingRpm(new ShiftpointRange(lowerShiftPoint, upperShiftPoint));
        }

        private void UpdateCurrentMode()
        {
            _service.KeepFollowingRpm(RangeForModes[ResponsivenessMode].AsModifiedBy(PercentageForAggressivenesLevel[AggressivenessLevel]));
        }
    }
}
