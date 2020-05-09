using System.Collections.Generic;
using GearboxDriver.Seedwork;

namespace GearboxDriver.Cabin.Transmission
{
    public class Lever
    {
        private TransmissionMode _currentMode;
        public bool HasEverReportedBefore { get; set; }

        public Lever()
        {
            _currentMode = TransmissionMode.Park;
        }

        public IReadOnlyCollection<IEvent> EnterDriveMode()
        {
            if (_currentMode == TransmissionMode.Drive && HasEverReportedBefore)
                throw new DomainRuleViolatedException("The car is already in drive mode.");

            _currentMode = TransmissionMode.Drive;

            HasEverReportedBefore = true;

            return new List<IEvent>{new DriveModeEntered()};
        }

        public IReadOnlyCollection<IEvent> EnterParkMode()
        {
            if (_currentMode == TransmissionMode.Park && HasEverReportedBefore)
                throw new DomainRuleViolatedException("The car is already in park mode.");

            _currentMode = TransmissionMode.Park;

            HasEverReportedBefore = true;

            return new List<IEvent> { new ParkModeEntered() };
        }

        public IReadOnlyCollection<IEvent> EnterReverseMode()
        {
            if (_currentMode == TransmissionMode.Reverse && HasEverReportedBefore)
                throw new DomainRuleViolatedException("The car is already in reverse mode.");

            _currentMode = TransmissionMode.Reverse;

            HasEverReportedBefore = true;

            return new List<IEvent> { new ReverseModeEntered() };
        }

        public IReadOnlyCollection<IEvent> EnterNeutralMode()
        {
            if (_currentMode == TransmissionMode.Neutral && HasEverReportedBefore)
                throw new DomainRuleViolatedException("The car is already in neutral mode.");

            _currentMode = TransmissionMode.Neutral;

            HasEverReportedBefore = true;

            return new List<IEvent> { new NeutralModeEntered() };
        }
    }
}
