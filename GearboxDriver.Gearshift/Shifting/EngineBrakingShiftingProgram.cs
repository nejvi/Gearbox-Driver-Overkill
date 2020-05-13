using System;
using GearboxDriver.Hardware.ACL;

namespace GearboxDriver.Gearshift.Shifting
{
    public class EngineBrakingShiftingProgram : IShiftingProgram
    {
        private readonly IShiftingProgram _innerProgram;

        public EngineBrakingShiftingProgram(IShiftingProgram innerProgram)
        {
            _innerProgram = innerProgram;
        }

        public SuggestedAction GetSuggestedActionFor(Gear currentGear, Rpm rpm)
        {
            if (_innerProgram.GetSuggestedActionFor(currentGear, rpm) == SuggestedAction.Retain)
                return SuggestedAction.Downshift;

            return SuggestedAction.Retain;
        }

    }
}
