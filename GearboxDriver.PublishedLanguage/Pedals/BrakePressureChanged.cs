using GearboxDriver.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace GearboxDriver.PublishedLanguage.Pedals
{
    public class BrakePressureChanged : IEvent
    {
        public PedalPressure PedalPressure { get; set; }

        public BrakePressureChanged(PedalPressure pedalPressure)
        {
            PedalPressure = pedalPressure;
        }

        public override string ToString()
        {
            return $"Brake pedal pressure changed to {PedalPressure.Value}.";
        }
    }
}
