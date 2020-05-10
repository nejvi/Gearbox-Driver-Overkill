using System;
using System.Collections.Generic;
using GearboxDriver.Seedwork;

namespace GearboxDriver.Cabin.Pedals
{
    public class PedalPanel
    {
        private PedalPressure CurrentGasPressure;
        private PedalPressure CurrentBrakePressure;

        public IReadOnlyCollection<IEvent> ChangeGasPressure(PedalPressure pressure)
        {
            return new List<IEvent> { new GasPressureChanged(pressure) };
        }

        public IReadOnlyCollection<IEvent> ChangeBrakePressure(PedalPressure pressure)
        {
            return new List<IEvent> { new BrakePressureChanged(pressure) };
        }
    }
}
