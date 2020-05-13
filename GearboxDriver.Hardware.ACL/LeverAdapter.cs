using GearboxDriver.Hardware.API;
using System;

namespace GearboxDriver.Hardware.ACL
{
    public class LeverAdapter : ILever
    {
        private Gearbox _gearbox;

        public LeverAdapter(Gearbox gearbox)
        {
            _gearbox = gearbox;
        }

        public void SetDriveMode()
        {
            _gearbox.setGearBoxCurrentParams(new Object[2] { 1, _gearbox.getCurrentGear()});
        }

        public void SetNeutralMode()
        {
            _gearbox.setGearBoxCurrentParams(new Object[2] { 4, 0 });
        }

        public void SetParkMode()
        {
            _gearbox.setGearBoxCurrentParams(new Object[2] { 2, 0 });
        }

        public void SetReverseMode()
        {
            _gearbox.setGearBoxCurrentParams(new Object[2] { 3, -1 });
        }
    }
}
