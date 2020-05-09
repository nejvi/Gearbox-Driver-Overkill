using GearboxDriver.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace GearboxDriver.Hardware.ACL
{
    public class VehicleTiltedDownhill : IEvent
    {
        public VehicleTiltPosition VehicleTiltPosition { get; }

        public VehicleTiltedDownhill(VehicleTiltPosition vehicleTiltPosition)
        {
            VehicleTiltPosition = vehicleTiltPosition;
        }
    }
}
