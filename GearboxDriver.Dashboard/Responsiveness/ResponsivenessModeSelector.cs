using System.Collections.Generic;
using GearboxDriver.Seedwork;

namespace GearboxDriver.Cabin.Responsiveness
{
    public class ResponsivenessModeSelector
    {
        private ResponsivenessMode _currentMode;
        private AggressivenessLevel _currentAggressivenessLevel;
        public bool HasEverReportedBefore { get; set; }

        public ResponsivenessModeSelector()
        {
            _currentMode = ResponsivenessMode.Comfort;
            _currentAggressivenessLevel = AggressivenessLevel.First;
        }

        public IReadOnlyCollection<IEvent> EnterEconomic()
        {
            if (_currentMode == ResponsivenessMode.Economic && HasEverReportedBefore)
                throw new DomainRuleViolatedException("Economic mode was already selected");

            _currentMode = ResponsivenessMode.Economic;

            HasEverReportedBefore = true;

            return new List<IEvent>{new EconomicModeEntered()};
        }

        public IReadOnlyCollection<IEvent> EnterComfort()
        {
            if (_currentMode == ResponsivenessMode.Comfort && HasEverReportedBefore)
                throw new DomainRuleViolatedException("Comfort mode was already selected");

            _currentMode = ResponsivenessMode.Comfort;

            HasEverReportedBefore = true;

            return new List<IEvent>{new ComfortModeEntered()};
        }

        public IReadOnlyCollection<IEvent> EnterSport()
        {
            if (_currentMode == ResponsivenessMode.Sport && HasEverReportedBefore)
                throw new DomainRuleViolatedException("Sport mode was already selected");

            _currentMode = ResponsivenessMode.Sport;

            HasEverReportedBefore = true;

            return new List<IEvent> {new SportModeEntered()};
        }

        public IReadOnlyCollection<IEvent> SetAggressivenessLevel(AggressivenessLevel level)
        {
            if(_currentAggressivenessLevel == level && HasEverReportedBefore)
                throw new DomainRuleViolatedException("This aggressiveness level was already selected");

            _currentAggressivenessLevel = level;

            HasEverReportedBefore = true;

            return new List<IEvent> { new AggressivenessLevelSelected(level) };
        }
    }
}
