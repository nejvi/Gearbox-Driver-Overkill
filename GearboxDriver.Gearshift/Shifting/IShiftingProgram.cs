﻿using GearboxDriver.Hardware.ACL;

namespace GearboxDriver.Gearshift.Shifting
{
    public interface IShiftingProgram
    {
        SuggestedAction GetSuggestedActionFor(Rpm rpm);
    }
}
