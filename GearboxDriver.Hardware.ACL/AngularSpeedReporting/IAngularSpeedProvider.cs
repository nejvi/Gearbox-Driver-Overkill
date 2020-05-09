using System;
using System.Collections.Generic;
using System.Text;

namespace GearboxDriver.Hardware.ACL.AngularSpeedReporting
{
    public interface IAngularSpeedProvider
    {
        AngularSpeed GetCurrentAngularSpeed();
    }
}
