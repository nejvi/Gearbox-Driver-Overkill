using GearboxDriver.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace GearboxDriver.Hardware.ACL
{
    public class VehicleTiledToStraightPosition : IEvent
    {
        public VehicleTiltPosition VehicleTiltPosition { get; }

        public VehicleTiledToStraightPosition(VehicleTiltPosition vehicleTiltPosition)
        {
            VehicleTiltPosition = vehicleTiltPosition;
        }
    }
}
