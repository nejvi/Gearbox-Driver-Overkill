using System;
using System.Collections.Generic;
using System.Text;

namespace GearboxDriver.Hardware.ACL.LightsReporting
{
    public interface ILightsPositionProvider
    {
        LightsPosition GetCurrentLights();
    }
}
