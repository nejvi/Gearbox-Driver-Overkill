using System;
using System.Collections.Generic;
using System.Text;

namespace GearboxDriver.Hardware.ACL
{
    public class LightsPositionChanged : IEvent
    {
        public LightsPosition LightsPosition { get; }

        public LightsPositionChanged(LightsPosition lightsPosition)
        {
            LightsPosition = lightsPosition;
        }
    }
}
