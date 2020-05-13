using System;
using GearboxDriver.Seedwork;

namespace GearboxDriver.SampleApplication
{
    class EventLogger : IEventListener
    {
        public void HandleEvent(IEvent @event)
        {
            Console.WriteLine($"[LOG]: {@event}");
        }
    }
}
