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
        private Rpm CurrentRpm { get; set; }

        public KickdownDetectionProcess(IKickdownCharacteristics kickdownCharacteristics, IGearshiftService gearshiftService)
        {
            _kickdownCharacteristics = kickdownCharacteristics;
            _gearshiftService = gearshiftService;
            ResponsivenessMode = ResponsivenessMode.Economic;
            CurrentGear = new Gear(0);
            LastGasPressure = new PedalPressure(0.0);
            CurrentRpm = new Rpm(0.0);
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
                case RpmChanged rpmChanged:
                    CurrentRpm = rpmChanged.NewRpm;
                    break;
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
            var test = _kickdownCharacteristics.GetActionFor(ResponsivenessMode, LastGasPressure, CurrentRpm);
            if (ShouldStopKickdown)
                StopKickdown();

            if (ShouldStartKickdown)
                StartKickdown();
        }

        private bool ShouldStopKickdown =>
            IsKickdownStarted && _kickdownCharacteristics.GetActionFor(ResponsivenessMode, LastGasPressure, CurrentRpm) == SuggestedKickdownAction.None;

        private void StopKickdown()
        {
            _gearshiftService.StopTargetingAnyGear();
            IsKickdownStarted = false;
        }

        private bool ShouldStartKickdown =>
            !IsKickdownStarted && _kickdownCharacteristics.GetActionFor(ResponsivenessMode, LastGasPressure, CurrentRpm) != SuggestedKickdownAction.None;

        private void StartKickdown()
        {
            switch (_kickdownCharacteristics.GetActionFor(ResponsivenessMode, LastGasPressure, CurrentRpm))
            {
                case SuggestedKickdownAction.Singular:
                    _gearshiftService.TargetGear(CurrentGear.DownshiftedBy(new Gear(1)));
                    break;
                case SuggestedKickdownAction.Double:
                    _gearshiftService.TargetGear(CurrentGear.DownshiftedBy(new Gear(2)));
                    break;
            }

            IsKickdownStarted = true;
        }
    }
}
