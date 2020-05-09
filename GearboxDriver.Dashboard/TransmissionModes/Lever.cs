using System.Collections.Generic;
using GearboxDriver.Seedwork;

namespace GearboxDriver.Dashboard.TransmissionModes
{
    public class Lever
    {
        private readonly TransmissionMode _currentMode;

        public Lever()
        {
            _currentMode = TransmissionMode.Park;
        }

        public IReadOnlyCollection<IEvent> EnterDriveMode()
        {
            if (_currentMode == TransmissionMode.Drive)
                throw new DomainRuleViolatedException("The car is already in drive mode.");

            return new List<IEvent>{new DriveModeEntered()};
        }

        public IReadOnlyCollection<IEvent> EnterParkMode()
        {
            if (_currentMode == TransmissionMode.Park)
                throw new DomainRuleViolatedException("The car is already in park mode.");

            return new List<IEvent> { new ParkModeEntered() };
        }

        public IReadOnlyCollection<IEvent> EnterReverseMode()
        {
            if (_currentMode == TransmissionMode.Reverse)
                throw new DomainRuleViolatedException("The car is already in reverse mode.");

            return new List<IEvent> { new ReverseModeEntered() };
        }

        public IReadOnlyCollection<IEvent> EnterNeutralMode()
        {
            if (_currentMode == TransmissionMode.Neutral)
                throw new DomainRuleViolatedException("The car is already in neutral mode.");

            return new List<IEvent> { new NeutralModeEntered() };
        }
    }
}
