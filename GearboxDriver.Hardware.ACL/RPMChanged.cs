using System;
using System.Collections.Generic;
using System.Text;

namespace GearboxDriver.Hardware.ACL
{
    public class RPMChanged : IEvent
    {
        public double Rpm { get; }

        public RPMChanged(double rpm)
        {
            Rpm = rpm;
        }
    }
}
