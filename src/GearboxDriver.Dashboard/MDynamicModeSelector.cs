using System.Collections.Generic;
using GearboxDriver.PublishedLanguage.MDynamic;
using GearboxDriver.Seedwork;

namespace GearboxDriver.CabinControls
{
    public class MDynamicModeSelector
    {
        private bool _enabled;

        public MDynamicModeSelector()
        {
            _enabled = false;
        }

        public IReadOnlyCollection<IEvent> Enable()
        {
            if (_enabled)
                throw new DomainRuleViolatedException("MDynamic mode is already enabled.");

            _enabled = true;

            return new List<IEvent>{new MDynamicModeEntered()};
        }

        public IReadOnlyCollection<IEvent> Disable()
        {
            if (!_enabled)
                throw new DomainRuleViolatedException("MDynamic mode is not enabled.");

            _enabled = false;

            return new List<IEvent> { new MDynamicModeEntered() };
        }
    }
}
