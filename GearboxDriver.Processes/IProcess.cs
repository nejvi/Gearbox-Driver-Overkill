using System;
using GearboxDriver.Seedwork;

namespace GearboxDriver.Processes
{
    public interface IProcess
    {
        void ApplyEvent(IEvent @event);
    }
}
