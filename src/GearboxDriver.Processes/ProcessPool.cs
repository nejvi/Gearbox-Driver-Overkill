using System;
using System.Collections.Generic;
using System.Linq;
using GearboxDriver.Seedwork;

namespace GearboxDriver.Processes
{
    public class ProcessPool : IEventListener
    {
        private List<IProcess> ProcessManagers { get; }

        public ProcessPool()
        {
            ProcessManagers = new List<IProcess>();
        }

        public void Add(IProcess process)
        {
            if (ProcessManagers.Any(x => x.GetType() == process.GetType()))
                throw new ArgumentException("Such process already exists in the pool.");

            ProcessManagers.Add(process);
        }

        public void Remove(Type processType)
        {
            var process = ProcessManagers.SingleOrDefault(x => x.GetType() == processType);

            if (process == null)
                throw new ArgumentException("Such process does not exist in the pool.");

            ProcessManagers.Remove(process);
        }


        public void HandleEvent(IEvent @event)
        {
            foreach (var manager in ProcessManagers)
                manager.ApplyEvent(@event);
        }
    }
}
