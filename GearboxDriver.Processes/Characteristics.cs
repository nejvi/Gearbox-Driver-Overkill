using GearboxDriver.Cabin.Responsiveness;
using GearboxDriver.Gearshift;
using GearboxDriver.Hardware.ACL;
using System.Collections.Generic;

namespace GearboxDriver.Processes
{
    public class Characteristics
    {
        private readonly Dictionary<ResponsivenessMode, ShiftpointRange> _rangeForMode =
            new Dictionary<ResponsivenessMode, ShiftpointRange>
            {
                { ResponsivenessMode.Economic, new ShiftpointRange(new Rpm(1000d), new Rpm(2000d)) },
                { ResponsivenessMode.Comfort, new ShiftpointRange(new Rpm(1000d), new Rpm(2500d)) },
                { ResponsivenessMode.Sport, new ShiftpointRange(new Rpm(1500d), new Rpm(5000d)) }
            };

        private readonly Dictionary<AggressivenessLevel, Percentage> _bumpPercentageForAggressivenessLevel =
            new Dictionary<AggressivenessLevel, Percentage>
            {
                { AggressivenessLevel.First, new Percentage(1.0d) },
                { AggressivenessLevel.Second, new Percentage(1.3d) },
                { AggressivenessLevel.Third, new Percentage(1.3d) }
            };

        public ShiftpointRange GetRangeFor(ResponsivenessMode responsivenessMode, AggressivenessLevel aggressivenessLevel)
            => _rangeForMode[responsivenessMode].AsModifiedBy(_bumpPercentageForAggressivenessLevel[aggressivenessLevel]);
    }
}
