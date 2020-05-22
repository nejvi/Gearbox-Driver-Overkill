using System.Linq;
using GearboxDriver.PublishedLanguage.MDynamic;
using GearboxDriver.Seedwork;
using NUnit.Framework;

namespace GearboxDriver.CabinControls.Tests
{
    public class MDynamicModeSelectorTests
    {
        [Test]
        public void EnableMDynamicResultsInEvent()
        {
            var mDynamic = new MDynamicModeSelector();

            var events = mDynamic.Enable();

            Assert.True(events.Any(x => x is MDynamicModeEntered));
        }

        [Test]
        public void DisableMDynamicResultsInEvent()
        {
            var mDynamic = new MDynamicModeSelector();

            mDynamic.Enable();
            var events = mDynamic.Disable();

            Assert.True(events.Any(x => x is MDynamicModeEntered));
        }

        [Test]
        public void CannotTurnOnMDynamicTwice()
        {
            var mDynamic = new MDynamicModeSelector();

            Assert.Throws<DomainRuleViolatedException>(() =>
            {
                mDynamic.Enable();
                mDynamic.Enable();
            });
        }

        [Test]
        public void CannotTurnOffMDynamicTwice()
        {
            var mDynamic = new MDynamicModeSelector();

            Assert.Throws<DomainRuleViolatedException>(() =>
            {
                mDynamic.Disable();
                mDynamic.Disable();
            });
        }
    }
}
