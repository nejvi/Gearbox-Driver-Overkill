using System.Collections.Generic;

namespace GearboxDriver.Seedwork
{
    public interface IEventBus
    {
        void SendEvent(IEvent @event);
        void SendEvent(IEnumerable<IEvent> events);
    }
}
