using System.Collections.Generic;
using System.Linq;
using GearboxDriver.Hardware.ACL;
using GearboxDriver.Seedwork;

namespace GearboxDriver.Gearshift
{
    public class QuantifiedShiftingProgram
    {
        private List<ShiftPoint> ShiftPointsAscending { get; }

        public QuantifiedShiftingProgram(IEnumerable<ShiftPoint> shiftPoints)
        {
            if (!shiftPoints.Any())
                throw new DomainRuleViolatedException("A shifting program needs to contain at least one shift point.");

            if (shiftPoints.Any(x => x.Threshold.Value <= 0))
                throw new DomainRuleViolatedException("A shifting program must provide at least one shiftpoint that supports 0 RPM.");

            if (shiftPoints.Select(x => x.Threshold).Distinct().Count() != shiftPoints.Select(x => x.Threshold).Count())
                throw new DomainRuleViolatedException("Shifting program cannot contain any shift points with the same threshold");

            ShiftPointsAscending = shiftPoints.OrderBy(x => x.Threshold.Value).ToList();
        }

        public Gear GearFor(RPM rpm)
        {
            var firstGearAbove = ShiftPointsAscending.FirstOrDefault(x => x.Threshold.Value >= rpm.Value);
            var firstGearBelowOrEqual = ShiftPointsAscending.Last(x => x.Threshold.Value <= rpm.Value);

            if (firstGearAbove == null)
                return ShiftPointsAscending.Last().Gear;

            return firstGearBelowOrEqual.Gear;
        }
    }
}
