using GearboxDriver.Gearshift;
using GearboxDriver.PublishedLanguage.Responsiveness;
using GearboxDriver.Seedwork;
namespace GearboxDriver.Processes
{
    public class ResponsivenessModeProgramUpdatingProcess : IProcess
    {
        private readonly IGearshiftService _service;
        private readonly EngineCharacteristics _characteristics;
        private ResponsivenessMode CurrentResponsivenessMode { get; set; }
        private AggressivenessLevel CurrentAggressivenessLevel { get; set; }

        public ResponsivenessModeProgramUpdatingProcess(IGearshiftService service, EngineCharacteristics characteristics)
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
