using GearboxDriver.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace GearboxDriver.Cabin.Pedals
{
    public class BrakePressureChanged : IEvent
    {
        public PedalPressure PedalPressure { get; set; }

        public BrakePressureChanged(PedalPressure pedalPressure)
        {
            PedalPressure = pedalPressure;
        }
    }
}
