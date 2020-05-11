using GearboxDriver.Gearshift.Shifting;
using GearboxDriver.Hardware.ACL;
using NUnit.Framework;

namespace GearboxDriver.Gearshift.Tests.Shifting
{
    class YieldingShiftingProgramTests
    {
        [Test]
        public void YieldsDisregardingTheGearAndRpm()
        {
            var program = new YieldingShiftingProgram();

            for (var i = 1; i < 5; i++)
                for(var y = 1000; y < 5000; y += 1000)
                    Assert.AreEqual(SuggestedAction.Retain, program.GetSuggestedActionFor(new Gear(i), new Rpm(y)));
        }
    }
}
