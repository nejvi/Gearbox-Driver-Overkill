using System;
using System.Collections.Generic;
using GearboxDriver.Seedwork;

namespace GearboxDriver.Cabin.Pedals
{
    public class PedalPanel
    {
        private double CurrentGasPedalPressure; // todo value object
        private double CurrentBrakePressure; // todo value object

        public IReadOnlyCollection<IEvent> ChangeGasPressure()
        {
            throw new NotImplementedException();
        }

        public IReadOnlyCollection<IEvent> ChangeBrakePressure()
        {
            throw new NotImplementedException();
        }
    }
}
