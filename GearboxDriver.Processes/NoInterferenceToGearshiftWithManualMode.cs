using GearboxDriver.Cabin.ManualGearshifting;
using GearboxDriver.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace GearboxDriver.Processes
{
    public class NoInterferenceToGearshiftWithManualMode : IProcess
    {
        private bool VehicleInManualMode { get; set; }

        public NoInterferenceToGearshiftWithManualMode()
        {
            VehicleInManualMode = false;
        }

        public void ApplyEvent(IEvent @event)
        {
            switch (@event)
            {
                case ManualGearshiftingModeEntered _:
                    if (!VehicleInManualMode)
                    {
                        VehicleInManualMode = true;
                    }
                    break;
                case ManualGearshiftingModeExited _:
                    VehicleInManualMode = false;
                    break;
            }
        }
    }
}
