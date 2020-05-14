using System;
using System.Collections.Generic;
using System.Text;

namespace GearboxDriver.Hardware.ACL.VehicleSlippingReporting
{
    public interface ISlippingSensor
    {
        bool IsCurrentlySlipping();
    }
}
