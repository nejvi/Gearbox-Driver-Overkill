using GearboxDriver.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace GearboxDriver.Cabin.Pedals
{
    public class GasPressureChanged : IEvent
    {
        public PedalPressure PedalPressure { get; set; }

        public GasPressureChanged(PedalPressure pedalPressure)
        {
            PedalPressure = pedalPressure;
        }
    }
}
