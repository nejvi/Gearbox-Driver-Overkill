using GearboxDriver.PublishedLanguage.Gearbox;
using GearboxDriver.PublishedLanguage.ManualGearshifting;
using GearboxDriver.PublishedLanguage.Responsiveness;
using GearboxDriver.PublishedLanguage.Transmission;
using GearboxDriver.Seedwork;

namespace GearboxDriver.CabinControls
{
    public class DashboardStateReadModel
    {
        public string CurrentTransmissionMode { get; private set; }
        public string CurrentRpm { get; private set; }
        public string CurrentResponsivenessMode { get; private set; }
        public string CurrentAggressivenessLevel { get; private set; }
        public bool IsInManualGearshiftingMode { get; private set; }

        public DashboardStateReadModel()
        {
            CurrentResponsivenessMode = "COMFORT";
            CurrentTransmissionMode = "PARK";
            CurrentResponsivenessMode = AggressivenessLevel.First.ToString();
            CurrentRpm = "0";
            IsInManualGearshiftingMode = false;
        }

        public void Apply(IEvent @event)
        {
            switch (@event)
            {
                case ParkModeEntered _:
                    CurrentTransmissionMode = "PARK";
                    break;
                case ReverseModeEntered _:
                    CurrentTransmissionMode = "REVERSE";
                    break;
                case NeutralModeEntered _:
                    CurrentTransmissionMode = "NEUTRAL";
                    break;
                case DriveModeEntered _:
                    CurrentTransmissionMode = "DRIVE";
                    break;

                case ComfortModeEntered _:
                    CurrentResponsivenessMode = "COMFORT";
                    break;
                case SportModeEntered _:
                    CurrentResponsivenessMode = "SPORT";
                    break;
                case EconomicModeEntered _:
                    CurrentResponsivenessMode = "ECONOMIC";
                    break;
                case AggressivenessLevelSelected aggressivenessLevelSelected:
                    CurrentAggressivenessLevel = aggressivenessLevelSelected.Level.ToString();
                    break;

                case RpmChanged rpmChanged:
                    CurrentRpm = rpmChanged.NewRpm.Value.ToString();
                    break;

                case ManualGearshiftingModeEntered _:
                    IsInManualGearshiftingMode = true;
                    break;
                case ManualGearshiftingModeExited _:
                    IsInManualGearshiftingMode = false;
                    break;
            }
        }
    }
}
