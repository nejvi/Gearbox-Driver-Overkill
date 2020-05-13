using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GearboxDriver.Seedwork;

namespace GearboxDriver.Processes
{
    public class ProcessPool : IEventListener
    {
        private List<IProcessManager> ProcessManagers { get; }

        public ProcessPool()
        {
            ProcessManagers = new List<IProcessManager>();
        }

        public void Add(IProcessManager processManager)
        {
            if (ProcessManagers.Any(x => x.GetType() == processManager.GetType()))
                throw new ArgumentException("Such process already exists in the pool.");

            ProcessManagers.Add(processManager);
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
