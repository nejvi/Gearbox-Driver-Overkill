using System;
using System.Collections.Generic;
using System.Text;
using GearboxDriver.Hardware.ACL;

namespace GearboxDriver.Gearshift
{
    public interface IGearshiftService
    {
        void AbstainFromChangingGears();
        void StopAbstainingFromChangingGears();
        void TargetGear(GearNumber gearNumber);
        void StopTargetingGear();
        void KeepFollowingRpm(Rpm lowerShiftpoint, Rpm upperShiftpoint);
        void ApplySharpnessFactor(double percentage);
    }
}
