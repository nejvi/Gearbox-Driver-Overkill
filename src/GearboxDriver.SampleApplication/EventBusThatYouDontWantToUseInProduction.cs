using System.Collections.Generic;
using System.Linq;
using GearboxDriver.Seedwork;

namespace GearboxDriver.SampleApplication
{
    public class EventBusThatYouDontWantToUseInProduction : IEventBus
    {
        private readonly List<IEventListener> _listeners;
        private bool _dead;

        public EventBusThatYouDontWantToUseInProduction()
        {
            _listeners = new List<IEventListener>();
        }

        public void SendEvent(IEvent @event)
        {
            if(!_dead)
                foreach (var listener in _listeners.ToList())
                 listener.HandleEvent(@event);
        }

        public void SendEvent(IEnumerable<IEvent> events)
        {
            foreach (var @event in events) 
                SendEvent(@event);
        }

        public void Attach(IEventListener listener)
        {
            _listeners.Add(listener);
        }

        public void Kill()
        {
            _dead = true;
        }
    }
}
