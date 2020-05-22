using System.Linq;
using GearboxDriver.PublishedLanguage.Transmission;
using NUnit.Framework;

namespace GearboxDriver.CabinControls.Tests
{
    public class LeverTests
    {
        [Test]
        public void EnteringDriveModeResultsInEvent()
        {
            var lever = new Lever();

            var events = lever.EnterDriveMode();

            Assert.True(events.Any(x => x is DriveModeEntered));
        }

        [Test]
        public void EnteringParkModeResultsInEvent()
        {
            var lever = new Lever();

            lever.EnterDriveMode();
            var events = lever.EnterParkMode();

            Assert.True(events.Any(x => x is ParkModeEntered));
        }

        [Test]
        public void EnteringReverseModeResultsInEvent()
        {
            var lever = new Lever();

            var events = lever.EnterReverseMode();

            Assert.True(events.Any(x => x is ReverseModeEntered));
        }

        [Test]
        public void EnteringNeutralModeResultsInEvent()
        {
            var lever = new Lever();

            var events = lever.EnterNeutralMode();

            Assert.True(events.Any(x => x is NeutralModeEntered));
        }
    }
}
