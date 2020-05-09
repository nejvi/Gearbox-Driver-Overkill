using GearboxDriver.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace GearboxDriver.Hardware.ACL
{
    public class VehicleTiltedUphill : IEvent
    {
        public VehicleTiltPosition VehicleTiltPosition { get; }

        public VehicleTiltedUphill(VehicleTiltPosition vehicleTiltPosition)
        {
            VehicleTiltPosition = vehicleTiltPosition;
        }
    }
}
