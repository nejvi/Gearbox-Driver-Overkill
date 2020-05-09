using System;
using System.Collections.Generic;
using System.Text;

namespace GearboxDriver.Hardware.ACL
{
    public class RPMChangedReporter
    {
        private readonly IEventBus _eventBus;

        public RPMChangedReporter(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void ReadRpm(double rpm)
        {

        }
    }
}
