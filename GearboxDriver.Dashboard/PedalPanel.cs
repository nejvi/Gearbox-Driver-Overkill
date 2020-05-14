using System.Collections.Generic;
using GearboxDriver.PublishedLanguage.Pedals;
using GearboxDriver.Seedwork;

namespace GearboxDriver.CabinControls
{
    public class PedalPanel
    {
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
