﻿using GearboxDriver.Hardware.ACL;

namespace GearboxDriver.Gearshift
{
    public interface IShiftingProgram
    {
        SuggestedAction GetSuggestedActionFor(Rpm rpm);
    }
}