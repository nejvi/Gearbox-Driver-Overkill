using System;
using System.Collections.Generic;
using System.Text;

namespace GearboxDriver.Hardware.ACL.VehicleSlippingReporting
{
    public interface ISlippingProvider
    {
        bool IsCurrentlySlipping();
    }
}
