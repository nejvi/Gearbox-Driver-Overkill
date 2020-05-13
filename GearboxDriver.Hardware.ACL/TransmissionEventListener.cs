using GearboxDriver.PublishedLanguage.Transmission;
using GearboxDriver.Seedwork;

namespace GearboxDriver.Hardware.ACL
{
    public class TransmissionEventListener : IEventListener
    {
        private readonly ILever _lever;

        public TransmissionEventListener(ILever lever)
        {
            _lever = lever;
        }

        public void HandleEvent(IEvent @event)
        {
            switch (@event)
            {
                case ParkModeEntered _:
                    _lever.SetParkMode();
                    break;
                case NeutralModeEntered _:
                    _lever.SetNeutralMode();
                    break;
                case DriveModeEntered _:
                    _lever.SetDriveMode();
                    break;
                case ReverseModeEntered _:
                    _lever.SetReverseMode();
                    break;
            }
        }

        public void HandleTransmission(IEvent @event)
        {

        }
    }
}
