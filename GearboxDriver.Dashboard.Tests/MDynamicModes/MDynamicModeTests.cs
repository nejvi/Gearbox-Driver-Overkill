using GearboxDriver.Cabin.MDynamic;
using GearboxDriver.Seedwork;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GearboxDriver.Dashboard.Tests.MDynamicModes
{
    public class MDynamicModeTests
    {
        [Test]
        public void EnableMDynamicResultsInEvent()
        {
            var mDynamic = new MDynamicMode();

            var events = mDynamic.Enable();

            Assert.True(events.Any(x => x is MDynamicModeEntered));
        }

        [Test]
        public void DisableMDynamicResultsInEvent()
        {
            var mDynamic = new MDynamicMode();

            mDynamic.Enable();
            var events = mDynamic.Disable();

            Assert.True(events.Any(x => x is MDynamicModeEntered));
        }

        [Test]
        public void CannotTurnOnMDynamicTwice()
        {
            var mDynamic = new MDynamicMode();

            Assert.Throws<DomainRuleViolatedException>(() =>
            {
                mDynamic.Enable();
                mDynamic.Enable();
            });
        }

        [Test]
        public void CannotTurnOffMDynamicTwice()
        {
            var mDynamic = new MDynamicMode();

            Assert.Throws<DomainRuleViolatedException>(() =>
            {
                mDynamic.Disable();
                mDynamic.Disable();
            });
        }
    }
}
