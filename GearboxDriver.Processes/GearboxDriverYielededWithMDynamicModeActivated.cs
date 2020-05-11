using GearboxDriver.Cabin.MDynamic;
using GearboxDriver.Gearshift;
using GearboxDriver.Hardware.ACL;
using GearboxDriver.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace GearboxDriver.Processes
{
    public class GearboxDriverYielededWithMDynamicModeActivated : IProcessManager
    {
        private IGearshiftService _service;

        public GearboxDriverYielededWithMDynamicModeActivated(IGearshiftService service)
        {
            _service = service;
        }
        
        public void ApplyEvent(IEvent @event)
        {
            switch (@event)
            {
                case VehicleStartedSlipping _:
                    _service.AbstainFromChangingGears();
                    break;
                case VehicleStoppedSlipping _:
                    _service.StopAbstainingFromChangingGears();
                    break;
            }
        }
    }
}
