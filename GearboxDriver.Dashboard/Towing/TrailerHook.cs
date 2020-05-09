using System.Collections.Generic;
using GearboxDriver.Seedwork;

namespace GearboxDriver.Cabin.Towing
{
    public class TrailerHook
    {
        private bool _trailerAttached { get; set; }

        public TrailerHook()
        {
            _trailerAttached = false;
        }

        public IReadOnlyCollection<IEvent> AttachTrailer()
        {
            if (_trailerAttached)
                throw new DomainRuleViolatedException("A trailer is already attached to the hook.");

            _trailerAttached = true;

            return new List<IEvent>{new TrailerHookBecameOccupied()};
        }

        public IReadOnlyCollection<IEvent> DetachTrailer()
        {
            if (!_trailerAttached)
                throw new DomainRuleViolatedException("The hook has no trailer attached.");

            _trailerAttached = false;

            return new List<IEvent> { new TrailerHookStoppedBeingOccupied() };
        }
    }
}
