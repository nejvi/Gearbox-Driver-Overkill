﻿using System;
using System.Collections.Generic;
using System.Text;
using GearboxDriver.Hardware.ACL;

namespace GearboxDriver.Gearshift
{
    public interface IGearshiftService
    {
        void AbstainFromChangingGears();
        void StopAbstainingFromChangingGears();
        void TargetGear(Gear gearNumber);
        void StopTargetingGear();
        void KeepFollowingRpm(ShiftpointRange shiftpointRange);
        void ApplySharpnessFactor(Percentage percentage);
    }
}