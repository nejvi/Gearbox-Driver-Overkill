using GearboxDriver.Gearshift.Shifting;
using GearboxDriver.Hardware.ACL;
using GearboxDriver.PublishedLanguage.Gearbox;
using NUnit.Framework;

namespace GearboxDriver.Gearshift.Tests.Shifting
{
    class YieldingShiftingProgramTests
    {
        [TestCase(1, 0)]
        [TestCase(2, 1542)]
        [TestCase(3, 2000)]
        [TestCase(4, 3000)]
        [TestCase(5, 4000)]
        [TestCase(6, 5555)]
        [TestCase(1, 7000)]
        [TestCase(2, 8751)]
        [TestCase(3, 90000)]
        [TestCase(4, 12121212)]
        [TestCase(5, 23232323)]
        [TestCase(12, 1231231231)]
        public void YieldsDisregardingTheGearAndRpm(int gear, int rpm)
        {
            var program = new YieldingShiftingProgram();
            
            Assert.AreEqual(SuggestedAction.Retain, program.GetSuggestedActionFor(new Gear(gear), new Rpm(rpm)));
        }
    }
}
