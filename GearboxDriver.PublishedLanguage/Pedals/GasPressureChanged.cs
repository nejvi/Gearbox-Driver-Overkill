using GearboxDriver.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace GearboxDriver.PublishedLanguage.Pedals
{
    public class GasPressureChanged : IEvent
    {
        public PedalPressure PedalPressure { get; set; }

        public GasPressureChanged(PedalPressure pedalPressure)
        {
            PedalPressure = pedalPressure;
        }

        public override string ToString()
        {
            return $"Gas pedal pressure changed to {PedalPressure.Value}.";
        }
    }
}
