using System.Collections.Generic;
using GearboxDriver.PublishedLanguage.Responsiveness;
using GearboxDriver.Seedwork;

namespace GearboxDriver.CabinControls
{
    public class ResponsivenessModeSelector
    {
        private ResponsivenessMode _currentMode;
        private AggressivenessLevel _currentAggressivenessLevel;

        public ResponsivenessModeSelector()
        {
            _currentMode = ResponsivenessMode.Comfort;
            _currentAggressivenessLevel = AggressivenessLevel.First;
        }

        public IReadOnlyCollection<IEvent> EnterEconomic()
        {
            if (_currentMode == ResponsivenessMode.Economic)
                throw new DomainRuleViolatedException("Economic mode was already selected");

            _currentMode = ResponsivenessMode.Economic;

            return new List<IEvent>{new EconomicModeEntered()};
        }

        public IReadOnlyCollection<IEvent> EnterComfort()
        {
            if (_currentMode == ResponsivenessMode.Comfort)
                throw new DomainRuleViolatedException("Comfort mode was already selected");

            _currentMode = ResponsivenessMode.Comfort;

            return new List<IEvent>{new ComfortModeEntered()};
        }

        public IReadOnlyCollection<IEvent> EnterSport()
        {
            if (_currentMode == ResponsivenessMode.Sport)
                throw new DomainRuleViolatedException("Sport mode was already selected");

            _currentMode = ResponsivenessMode.Sport;

            return new List<IEvent> {new SportModeEntered()};
        }

        public IReadOnlyCollection<IEvent> SetAggressivenessLevel(AggressivenessLevel level)
        {
            if(_currentAggressivenessLevel == level)
                throw new DomainRuleViolatedException("This aggressiveness level was already selected");

            _currentAggressivenessLevel = level;

            return new List<IEvent> { new AggressivenessLevelSelected(level) };
        }
    }
}
