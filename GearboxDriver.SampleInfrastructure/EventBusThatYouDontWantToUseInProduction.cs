using System;
using System.Collections.Generic;
using System.Linq;
using GearboxDriver.Hardware.ACL;
using GearboxDriver.Seedwork;

namespace GearboxDriver.SampleInfrastructure
{
    public class EventBusThatYouDontWantToUseInProduction : IEventBus
    {
        private readonly List<IEventListener> _listeners;

        public EventBusThatYouDontWantToUseInProduction()
        {
            _listeners = new List<IEventListener>();
        }

        public void SendEvent(IEvent @event)
        {
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
    }
}
