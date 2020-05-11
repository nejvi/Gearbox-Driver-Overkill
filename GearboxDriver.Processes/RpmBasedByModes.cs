using GearboxDriver.Cabin.Responsiveness;
using GearboxDriver.Gearshift;
using GearboxDriver.Seedwork;

namespace GearboxDriver.Processes
{
    public class RpmBasedByModes : IProcessManager
    {
        private readonly IGearshiftService _service;
        private readonly Characteristics _characteristics;
        private ResponsivenessMode CurrentResponsivenessMode { get; set; }
        private AggressivenessLevel CurrentAggressivenessLevel { get; set; }

        public RpmBasedByModes(IGearshiftService service, Characteristics characteristics)
        {
            _service = service;
            _characteristics = characteristics;
        }

        public void ApplyEvent(IEvent @event)
        {
            switch (@event)
            {
                case ComfortModeEntered _:
                    CurrentResponsivenessMode = ResponsivenessMode.Comfort;
                    break;
                case EconomicModeEntered _:
                    CurrentResponsivenessMode = ResponsivenessMode.Economic;
                    break;
                case SportModeEntered _:
                    CurrentResponsivenessMode = ResponsivenessMode.Sport;
                    break;
                case AggressivenessLevelSelected aggressivenessLevel:
                    CurrentAggressivenessLevel = aggressivenessLevel.Level;
                    break;
            }

            UpdateCurrentMode();
        }

        private void UpdateCurrentMode() 
            => _service.KeepFollowingRpm(_characteristics.GetRangeFor(CurrentResponsivenessMode, CurrentAggressivenessLevel));
    }
}
