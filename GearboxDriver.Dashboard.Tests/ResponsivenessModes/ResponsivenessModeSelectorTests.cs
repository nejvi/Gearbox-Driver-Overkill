﻿using GearboxDriver.Cabin.Responsiveness;
using GearboxDriver.Seedwork;
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
            var selector = new ResponsivenessModeSelector();

            var events = selector.EnterEconomic();

            Assert.True(events.Any(x => x is EconomicModeEntered));
        }

        [Test]
        public void EnteringComfortModeResultsInEvent()
        {
            var selector = new ResponsivenessModeSelector();

            var events = selector.EnterComfort();

            Assert.True(events.Any(x => x is ComfortModeEntered));
        }

        [Test]
        public void EnteringSportModeResultsInEvent()
        {
            var selector = new ResponsivenessModeSelector();

            var events = selector.EnterSport();

            Assert.True(events.Any(x => x is SportModeEntered));
        }

        [Test]
        public void SettingAggressivenessLevelResultsInEvent()
        {
            var selector = new ResponsivenessModeSelector();

            var events = selector.SetFirstAggressivenessLevel(AggressivenessLevel.Second);

            Assert.True(events.Any(x => x is AggressivenessLevelSelected));
        }

        [Test]
        public void CannotEnterSportModeTwice()
        {
            var selector = new ResponsivenessModeSelector();

            Assert.Throws<DomainRuleViolatedException>(() =>
            {
                selector.EnterSport();
                selector.EnterSport();
            });
        }

        [Test]
        public void CannotEnterComfortModeTwice()
        {
            var selector = new ResponsivenessModeSelector();

            Assert.Throws<DomainRuleViolatedException>(() =>
            {
                selector.EnterComfort();
                selector.EnterComfort();
            });
        }

        [Test]
        public void CannotEnterEconomicModeTwice()
        {
            var selector = new ResponsivenessModeSelector();

            Assert.Throws<DomainRuleViolatedException>(() =>
            {
                selector.EnterEconomic();
                selector.EnterEconomic();
            });
        }

        [Test]
        public void CannotSetSameAggressivenessLevelTwice()
        {
            var selector = new ResponsivenessModeSelector();

            Assert.Throws<DomainRuleViolatedException>(() =>
            {
                selector.SetFirstAggressivenessLevel(AggressivenessLevel.First);
                selector.SetFirstAggressivenessLevel(AggressivenessLevel.First);
            });
        }
    }
}
