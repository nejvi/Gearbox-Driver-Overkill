using System;
using System.Collections.Generic;
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
            foreach (var listener in _listeners)
                listener.SendEvent(@event);
        }

        public void SubscribeForEvents(IEventListener listener)
        {
            _listeners.Add(listener);
        }
    }
}
