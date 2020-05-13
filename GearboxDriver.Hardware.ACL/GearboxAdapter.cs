using GearboxDriver.Hardware.API;
using GearboxDriver.Seedwork;
using GearboxDriver.PublishedLanguage.Transmission;
using System;
using GearboxDriver.PublishedLanguage.ManualGearshifting;

namespace GearboxDriver.Hardware.ACL
{
    public class GearboxAdapter 
    {
        private readonly Gearbox _gearbox;

        public GearboxAdapter(Gearbox gearbox)
        {
            _gearbox = gearbox;
        }



        public void SetCurrentGear(IEvent @event)
        {
            switch (@event)
            {
                case GearDownshiftedManually _:
                    _gearbox.setCurrentGear((int)_gearbox.getCurrentGear() - 1);
                    break;
                case GearUpshiftedManually _:
                    _gearbox.setCurrentGear((int)_gearbox.getCurrentGear() + 1);
                    break;
            }
        }
    }
}
