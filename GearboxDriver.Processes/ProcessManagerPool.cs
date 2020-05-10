using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GearboxDriver.Seedwork;

namespace GearboxDriver.Processes
{
    public class ProcessManagerPool
    {
        private List<IProcessManager> _processManagers { get; }

        public void Add(IProcessManager processManager)
        {
            if (_processManagers.Any(x => x.GetType() == processManager.GetType()))
                throw new ArgumentException("Such process already exists in the pool.");

            _processManagers.Add(processManager);
        }

        public void Remove(Type processType)
        {
            var process = _processManagers.SingleOrDefault(x => x.GetType() == processType);

            if (process == null)
                throw new ArgumentException("Such process does not exist in the pool.");

            _processManagers.Remove(process);
        }

        public void Dispatch(IEvent @event)
        {
            foreach (var manager in _processManagers)
                manager.ApplyEvent(@event);
        }
    }
}
