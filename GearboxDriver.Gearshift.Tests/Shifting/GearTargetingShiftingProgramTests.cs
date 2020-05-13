using GearboxDriver.Gearshift.Shifting;
using GearboxDriver.Hardware.ACL;
using GearboxDriver.PublishedLanguage.Gearbox;
using NUnit.Framework;

namespace GearboxDriver.Gearshift.Tests.Shifting
{
    class GearTargetingShiftingProgramTests
    {
        private readonly Rpm _irrelevantRpm = new Rpm(1000);

        [Test]
        public void SuggestsRetainingWhenCurrentGearIsEqualToTargetedOne()
        {
            var program = new GearTargetingShiftingProgram(new Gear(3));

            var suggestedAction = program.GetSuggestedActionFor(new Gear(3), _irrelevantRpm);

            Assert.AreEqual(SuggestedAction.Retain, suggestedAction);
        }

        [Test]
        public void SuggestsUpshiftingWhenCurrentGearIsLowerThanTheTargetedOne()
        {
            var program = new GearTargetingShiftingProgram(new Gear(5));

            var suggestedAction = program.GetSuggestedActionFor(new Gear(3), _irrelevantRpm);

            Assert.AreEqual(SuggestedAction.Upshift, suggestedAction);
        }

        [Test]
        public void SuggestsDownshiftingWhenCurrentGearIsHigherThanTargetedOne()
        {
            var program = new GearTargetingShiftingProgram(new Gear(2));

            var suggestedAction = program.GetSuggestedActionFor(new Gear(3), _irrelevantRpm);

            Assert.AreEqual(SuggestedAction.Downshift, suggestedAction);
        }
    }
}
