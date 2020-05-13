using GearboxDriver.Hardware.API;
using System;

namespace GearboxDriver.Hardware.ACL
{
    public class LeverAdapter : ILever
    {
        private readonly Gearbox _gearbox;

        public LeverAdapter(Gearbox gearbox)
        {
            _gearbox = gearbox;
        }

        public void SetDriveMode()
        {
            _gearbox.setGearBoxCurrentParams(new Object[2] { 1, CurrentGearOrFallback });
        }

        public void SetNeutralMode()
        {
            _gearbox.setGearBoxCurrentParams(new Object[2] { 4, CurrentGearOrFallback });
        }

        public void SetParkMode()
        {
            _gearbox.setGearBoxCurrentParams(new Object[2] { 2, CurrentGearOrFallback });
        }

        public void SetReverseMode()
        {
            _gearbox.setGearBoxCurrentParams(new Object[2] { 3, CurrentGearOrFallback });
        }

        private object CurrentGearOrFallback => _gearbox.getCurrentGear() ?? 0;
    }
}
