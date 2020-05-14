using GearboxDriver.Gearshift;
using GearboxDriver.PublishedLanguage;
using GearboxDriver.PublishedLanguage.Gearbox;
using GearboxDriver.PublishedLanguage.Pedals;
using GearboxDriver.PublishedLanguage.Responsiveness;
using GearboxDriver.Seedwork;

namespace GearboxDriver.Processes
{
    public class KickdownDetectionProcess : IProcess
    {
        private readonly IKickdownCharacteristics _kickdownCharacteristics;
        private readonly IGearshiftService _gearshiftService;
        private ResponsivenessMode ResponsivenessMode { get; set; }
        private PedalPressure LastGasPressure { get; set; }
        private bool IsKickdownStarted { get; set; }
        private Gear CurrentGear { get; set; }

        public KickdownDetectionProcess(IKickdownCharacteristics kickdownCharacteristics, IGearshiftService gearshiftService)
        {
            _kickdownCharacteristics = kickdownCharacteristics;
            _gearshiftService = gearshiftService;
            ResponsivenessMode = ResponsivenessMode.Economic;
            CurrentGear = new Gear(0);
        }

        public void ApplyEvent(IEvent @event)
        {
            UpdateState(@event);
            Act();
        }

        private void UpdateState(IEvent @event)
        {
            switch (@event)
            {
                case GasPressureChanged gasPressure:
                    LastGasPressure = gasPressure.PedalPressure;
                    break;
                case GearChanged gearChanged:
                    CurrentGear = gearChanged.EnteredGear;
                    break;
                case EconomicModeEntered _:
                    ResponsivenessMode = ResponsivenessMode.Economic;
                    break;
                case ComfortModeEntered _:
                    ResponsivenessMode = ResponsivenessMode.Comfort;
                    break;
                case SportModeEntered _:
                    ResponsivenessMode = ResponsivenessMode.Sport;
                    break;
            }
        }

        private void Act()
        {
            if (ShouldStopKickdown)
                StopKickdown();

            if (ShouldStartKickdown)
                StartKickdown();
        }

        private bool ShouldStopKickdown =>
            IsKickdownStarted && _kickdownCharacteristics.GetActionFor(ResponsivenessMode, LastGasPressure) == SuggestedKickdownAction.None;

        private void StopKickdown()
        {
            _gearshiftService.StopTargetingAnyGear();
            IsKickdownStarted = false;
        }

        private bool ShouldStartKickdown =>
            !IsKickdownStarted && _kickdownCharacteristics.GetActionFor(ResponsivenessMode, LastGasPressure) != SuggestedKickdownAction.None;

        private void StartKickdown()
        {
            switch (_kickdownCharacteristics.GetActionFor(ResponsivenessMode, LastGasPressure))
            {
                case SuggestedKickdownAction.Singular:
                    _gearshiftService.TargetGear(CurrentGear.DownshiftedBy(new Gear(1)));
                    break;
                case SuggestedKickdownAction.Double:
                    _gearshiftService.TargetGear(CurrentGear.DownshiftedBy(new Gear(2)));
                    break;
            }
            
        }
    }
}
