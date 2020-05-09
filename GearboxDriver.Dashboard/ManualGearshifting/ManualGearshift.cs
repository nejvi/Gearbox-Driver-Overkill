using System.Collections.Generic;
using GearboxDriver.Seedwork;

namespace GearboxDriver.Cabin.ManualGearshifting
{
    public class ManualGearshift
    {
        private int _currentGear { get; set; } // todo value objects
        private int _maxGear { get; set; }
        private int _minGear { get; set; }
        private bool _inManualMode { get; set; }

        public ManualGearshift(int currentGear, int maxGear, int minGear, bool inManualMode)
        {
            _currentGear = currentGear;
            _maxGear = maxGear;
            _minGear = minGear;
            _inManualMode = inManualMode;
        }

        public IReadOnlyCollection<IEvent> Upshift()
        {
            if (_currentGear == _maxGear)
                throw new DomainRuleViolatedException("Cannot upshift - the current gear is the maximal one.");

            _currentGear++;
            return new List<IEvent>{ new GearUpshiftedManually() };
        }

        public IReadOnlyCollection<IEvent> Downshift()
        {
            if (_currentGear == _minGear)
                throw new DomainRuleViolatedException("Cannot upshift - the current gear is the minimal one.");

            _currentGear--;
            return new List<IEvent> { new GearDownshiftedManually() };
        }

        public IReadOnlyCollection<IEvent> EnterManualMode()
        {
            if (_inManualMode)
                throw new DomainRuleViolatedException("Manual mode is already entered.");

            _inManualMode = true;
            return new List<IEvent> { new ManualGearshiftingModeEntered() };
        }

        public IReadOnlyCollection<IEvent> ExitManualMode()
        {
            if (!_inManualMode)
                throw new DomainRuleViolatedException("Manual mode is not enabled.");

            _inManualMode = false;
            return new List<IEvent> { new ManualGearshiftingModeExited() };
        }
    }
}
