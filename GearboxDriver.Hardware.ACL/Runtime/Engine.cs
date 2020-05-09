using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GearboxDriver.Hardware.ACL.Runtime
{
    public class Engine
    {
        private TimeSpan _tickRate;
        private IReadOnlyCollection<IReporter> _reporters;
        private bool _isRunning;

        public Engine(IReadOnlyCollection<IReporter> reporters)
        {
            if (reporters.Count == 0)
                throw new EngineStartUpException("Engine has no reporters");

            _reporters = reporters;
        }

        public void Start()
        {
            if(_isRunning)
                throw new EngineStartUpException("Engine is already running.");

            _isRunning = true;

            Task.Run(Run);
        }

        private void Run()
        {
            while (true)
            {
                foreach (IReporter reporter in _reporters)
                    reporter.Report();

                Task.Delay(_tickRate).Wait();
            }
        }
    }
}
