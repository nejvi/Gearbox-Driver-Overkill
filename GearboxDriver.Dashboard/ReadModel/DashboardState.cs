using GearboxDriver.Dashboard.Responsiveness;
using GearboxDriver.Dashboard.Transmission;
using GearboxDriver.Hardware.ACL;
using GearboxDriver.Seedwork;

namespace GearboxDriver.Dashboard.ReadModel
{
    public class DashboardState
    {
        public string CurrentTransmissionMode { get; private set; }
        public string CurrentRPM { get; private set; }
        public string CurrentResponsivenessMode { get; private set; }
        public string CurrentAggressivenessLevel { get; private set; }

        public DashboardState()
        {
            CurrentResponsivenessMode = "COMFORT";
            CurrentTransmissionMode = "PARK";
            CurrentResponsivenessMode = AggressivenessLevel.First.ToString();
            CurrentRPM = "0";
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
                    CurrentAggressivenessLevel = aggressivenessLevelSelected.ToString();
                    break;

                case RPMChanged rpmChanged:
                    CurrentRPM = rpmChanged.Rpm.Value.ToString();
                    break;
            }
        }
    }
}
