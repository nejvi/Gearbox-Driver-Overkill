using System.Collections.Generic;
using GearboxDriver.Seedwork;

namespace GearboxDriver.Cabin.Transmission
{
    public class Lever
    {
        private TransmissionMode _currentMode;

        public Lever()
        {
            _currentMode = TransmissionMode.Park;
        }

        public IReadOnlyCollection<IEvent> EnterDriveMode()
        {
            if (_currentMode == TransmissionMode.Drive)
                throw new DomainRuleViolatedException("The car is already in drive mode.");

            _currentMode = TransmissionMode.Drive;

            return new List<IEvent>{new DriveModeEntered()};
        }

        public IReadOnlyCollection<IEvent> EnterParkMode()
        {
            if (_currentMode == TransmissionMode.Park)
                throw new DomainRuleViolatedException("The car is already in park mode.");

            _currentMode = TransmissionMode.Park;

            return new List<IEvent> { new ParkModeEntered() };
        }

        public IReadOnlyCollection<IEvent> EnterReverseMode()
        {
            if (_currentMode == TransmissionMode.Reverse)
                throw new DomainRuleViolatedException("The car is already in reverse mode.");

            _currentMode = TransmissionMode.Reverse;

            return new List<IEvent> { new ReverseModeEntered() };
        }

        public IReadOnlyCollection<IEvent> EnterNeutralMode()
        {
            if (_currentMode == TransmissionMode.Neutral)
                throw new DomainRuleViolatedException("The car is already in neutral mode.");

            _currentMode = TransmissionMode.Neutral;

            return new List<IEvent> { new NeutralModeEntered() };
        }
    }
}
