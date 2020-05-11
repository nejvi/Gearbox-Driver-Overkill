using System;
using System.Collections.Generic;
using System.Text;

namespace GearboxDriver.Hardware.ACL.Runtime
{
    public class EngineBuilder
    {
        private List<IReporter> _reporters;

        public EngineBuilder()
        {
            _reporters = new List<IReporter>();
        }

        public EngineBuilder AddReporter(IReporter reporter)
        {
            _reporters.Add(reporter);

            return this;
        }

        public Engine Build()
        {
            return new Engine(_reporters);
        }
    }
}
