using GearboxDriver.Cabin.Responsiveness;
using GearboxDriver.Gearshift;
using GearboxDriver.Hardware.ACL;
using GearboxDriver.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace GearboxDriver.Processes
{
    public class RpmBasedByModes : IProcessManager
    {
        private IGearshiftService _service;
        private ResponsivenessMode ResponsivenessMode { get; set; }
        private AggressivenessLevel AggressivenessLevel { get; set; }

        public RpmBasedByModes(IGearshiftService service)
        {
            _service = service;
        }

        public void ApplyEvent(IEvent @event)
        {
            switch (@event)
            {
                case ComfortModeEntered _:
                    RpmBasedByComfortMode();
                    break;
                case EconomicModeEntered _:
                    RpmBasedByEconomicMode();
                    break;
                case SportModeEntered _:
                    RpmBasedBySportMode();
                    break;
                case AggressivenessLevelSelected aggressivenessLevel:
                    RpmBasedByAggressivenessLevel(aggressivenessLevel.Level);
                    break;
            }
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

        public void RpmBasedByEconomicMode(double modificator = 1)
        {
            ResponsivenessMode = ResponsivenessMode.Economic;

            var lowerShiftPoint = new Rpm(1000d * modificator);
            var upperShiftPoint = new Rpm(2000d * modificator);

            _service.KeepFollowingRpm(new ShiftpointRange(lowerShiftPoint, upperShiftPoint));
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
    }
}
