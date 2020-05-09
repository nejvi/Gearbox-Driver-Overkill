using GearboxDriver.Cabin.Responsiveness;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GearboxDriver.Dashboard.Tests.ResponsivenessModes
{
    public class ResponsivenessModeSelectorTests
    {
        [Test]
        public void EnteringEconomicModeResultsInEvent()
        {
            var selector = new ResponsivenessModeSelector(ResponsivenessMode.Comfort, AggressivenessLevel.First);

            var events = selector.EnterEconomic();

            Assert.True(events.Any(x => x is EconomicModeEntered));
        }

        [Test]
        public void EnteringComfortModeResultsInEvent()
        {
            var selector = new ResponsivenessModeSelector(ResponsivenessMode.Economic, AggressivenessLevel.First);

            var events = selector.EnterComfort();

            Assert.True(events.Any(x => x is ComfortModeEntered));
        }

        [Test]
        public void EnteringSportModeResultsInEvent()
        {
            var selector = new ResponsivenessModeSelector(ResponsivenessMode.Comfort, AggressivenessLevel.First);

            var events = selector.EnterSport();

            Assert.True(events.Any(x => x is SportModeEntered));
        }

        [Test]
        public void SettingAggressivenessLevelResultsInEvent()
        {
            var selector = new ResponsivenessModeSelector(ResponsivenessMode.Comfort, AggressivenessLevel.First);

            var events = selector.SetFirstAggressivenessLevel(AggressivenessLevel.Second);

            Assert.True(events.Any(x => x is AggressivenessLevelSelected));
        }
    }
}
