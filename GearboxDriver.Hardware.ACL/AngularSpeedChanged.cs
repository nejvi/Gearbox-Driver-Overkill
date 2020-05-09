using GearboxDriver.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace GearboxDriver.Hardware.ACL
{
    public class AngularSpeedChanged : IEvent
    {
        public AngularSpeed AngularSpeed { get; }

        public AngularSpeedChanged(AngularSpeed angularSpeed)
        {
            AngularSpeed = angularSpeed;
        }
    }
}
