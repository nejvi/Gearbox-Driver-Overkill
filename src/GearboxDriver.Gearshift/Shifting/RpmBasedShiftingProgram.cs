﻿using GearboxDriver.PublishedLanguage.Gearbox;

namespace GearboxDriver.Gearshift.Shifting
{
    public class RpmBasedShiftingProgram : IShiftingProgram
    {
        private ShiftpointRange Range { get; }

        public RpmBasedShiftingProgram(ShiftpointRange range)
        {
            Range = range;
        }

        public SuggestedAction GetSuggestedActionFor(Gear currentGear, Rpm rpm)
        {
            if (rpm.Value < Range.LowerShiftPoint.Value) return SuggestedAction.Downshift;

            if (rpm.Value > Range.UpperShiftPoint.Value) return SuggestedAction.Upshift;

            return SuggestedAction.Retain;
        }

    }
}
