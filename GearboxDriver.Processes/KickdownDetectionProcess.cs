using GearboxDriver.PublishedLanguage;
using GearboxDriver.PublishedLanguage.Pedals;
using GearboxDriver.PublishedLanguage.Responsiveness;
using GearboxDriver.Seedwork;

namespace GearboxDriver.Processes
{
    public class KickdownDetectionProcess : IProcess
    {
        private readonly IKickdownCharacteristics _kickdownCharacteristics;
        private ResponsivenessMode ResponsivenessMode { get; set; }
        private PedalPressure LastGasPressure { get; set; }

        public KickdownDetectionProcess(IKickdownCharacteristics kickdownCharacteristics)
        {
            _kickdownCharacteristics = kickdownCharacteristics;
            ResponsivenessMode = ResponsivenessMode.Economic;
        }

        public void ApplyEvent(IEvent @event)
        {
            switch (@event)
            {
                case GasPressureChanged gasPressure:
                    LastGasPressure = gasPressure.PedalPressure;
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

            _kickdownCharacteristics.GetActionFor(ResponsivenessMode, LastGasPressure);
        }
    }
}
