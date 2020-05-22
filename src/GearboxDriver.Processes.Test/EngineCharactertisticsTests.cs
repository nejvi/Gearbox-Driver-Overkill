using GearboxDriver.PublishedLanguage.Responsiveness;
using NUnit.Framework;

namespace GearboxDriver.Processes.Test
{
    public class EngineCharactertisticsTests
    {
        [TestCase(ResponsivenessMode.Economic, AggressivenessLevel.First , 1000d, 2000d)]
        [TestCase(ResponsivenessMode.Economic, AggressivenessLevel.Second , 1300d, 2600d)]
        [TestCase(ResponsivenessMode.Economic, AggressivenessLevel.Third , 1300d, 2600d)]
        [TestCase(ResponsivenessMode.Comfort, AggressivenessLevel.First, 1000d, 2500d)]
        [TestCase(ResponsivenessMode.Comfort, AggressivenessLevel.Second, 1300d, 3250d)]
        [TestCase(ResponsivenessMode.Comfort, AggressivenessLevel.Third, 1300d, 3250d)]
        [TestCase(ResponsivenessMode.Sport, AggressivenessLevel.First, 1500d, 5000d)]
        [TestCase(ResponsivenessMode.Sport, AggressivenessLevel.Second, 1950d, 6500d)]
        [TestCase(ResponsivenessMode.Sport, AggressivenessLevel.Third, 1950d, 6500d)]
        public void ReturnsProperRangeForModeAndAggressivenessLevel(ResponsivenessMode selectedMode, AggressivenessLevel selectedAggressiveness, double lowerShiftPoint, double upperShiftPoint)
        {
            var characteristics = new EngineCharacteristics();

            var characteristicsRange = characteristics.GetRangeFor(selectedMode, selectedAggressiveness);

            Assert.AreEqual(lowerShiftPoint, characteristicsRange.LowerShiftPoint.Value);
            Assert.AreEqual(upperShiftPoint, characteristicsRange.UpperShiftPoint.Value);
        }
    }
}
