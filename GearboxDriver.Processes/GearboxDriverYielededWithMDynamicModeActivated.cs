using GearboxDriver.Cabin.MDynamic;
using GearboxDriver.Hardware.ACL;
using GearboxDriver.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace GearboxDriver.Processes
{
    public class GearboxDriverYielededWithMDynamicModeActivated : IProcessManager
    {
        public void ApplyEvent(IEvent @event)
        {
            switch (@event)
            {
                case VehicleStartedSlipping _:
                    //Send information to negotiator
                    break;
                case VehicleStoppedSlipping _:
                    //Send information to negotiator
                    break;
            }
        }
    }
}
