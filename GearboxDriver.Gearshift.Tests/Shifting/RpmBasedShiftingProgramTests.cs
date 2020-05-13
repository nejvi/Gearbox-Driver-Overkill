using System.Diagnostics.CodeAnalysis;
using GearboxDriver.Gearshift.Shifting;
using GearboxDriver.Hardware.ACL;
using GearboxDriver.PublishedLanguage.Gearbox;
using NUnit.Framework;

namespace GearboxDriver.Gearshift.Tests.Shifting
{
    class RpmBasedShiftingProgramTests
    {
        private static readonly Rpm BelowLowerValue = new Rpm(1000);
        private static readonly Rpm LowerValue = new Rpm(2000);
        private static readonly Rpm MiddleValue = new Rpm(3000);
        private static readonly Rpm UpperValue = new Rpm(4000);
        private static readonly Rpm AboveUpperValue = new Rpm(5000);
        private static readonly ShiftpointRange Range = new ShiftpointRange(LowerValue, UpperValue);
        private static readonly Gear IrrelevantGear = new Gear(3);

        [Test]
        public void SuggestsRetainingTheCurrentGearWhenWithinRange()
        {
            var program = new RpmBasedShiftingProgram(Range);

            var suggestedAction = program.GetSuggestedActionFor(IrrelevantGear, MiddleValue);

            Assert.AreEqual(SuggestedAction.Retain, suggestedAction);
        }

        [Test]
        public void SuggestsRetainingTheCurrentGearWhenAboveRange()
        {
            var program = new RpmBasedShiftingProgram(Range);

            var suggestedAction = program.GetSuggestedActionFor(IrrelevantGear, AboveUpperValue);

            Assert.AreEqual(SuggestedAction.Upshift, suggestedAction);
        }

        [Test]
        public void SuggestsRetainingTheCurrentGearWhenBelowRange()
        {
            var program = new RpmBasedShiftingProgram(Range);

            var suggestedAction = program.GetSuggestedActionFor(IrrelevantGear, BelowLowerValue);

            Assert.AreEqual(SuggestedAction.Downshift, suggestedAction);
        }
    }
}
