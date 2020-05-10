using System;
using GearboxDriver.Seedwork;

namespace GearboxDriver.Processes
{
    public interface IProcessManager
    {
        void ApplyEvent(IEvent @event);
    }
}
