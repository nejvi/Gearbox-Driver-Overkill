using System.Collections.Generic;
using GearboxDriver.PublishedLanguage.Towing;
using GearboxDriver.Seedwork;

namespace GearboxDriver.CabinControls
{
    public class TrailerHook
    {
        private bool TrailerAttached { get; set; }

        public TrailerHook()
        {
            TrailerAttached = false;
        }

        public IReadOnlyCollection<IEvent> AttachTrailer()
        {
            if (TrailerAttached)
                throw new DomainRuleViolatedException("A trailer is already attached to the hook.");

            TrailerAttached = true;

            return new List<IEvent>{new TrailerHookBecameOccupied()};
        }

        public IReadOnlyCollection<IEvent> DetachTrailer()
        {
            if (!TrailerAttached)
                throw new DomainRuleViolatedException("The hook has no trailer attached.");

            TrailerAttached = false;

            return new List<IEvent> { new TrailerHookStoppedBeingOccupied() };
        }
    }
}
