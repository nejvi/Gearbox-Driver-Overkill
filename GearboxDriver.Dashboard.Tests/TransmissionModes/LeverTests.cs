using System.Linq;
using GearboxDriver.Dashboard.Transmission;
using NUnit.Framework;

namespace GearboxDriver.Dashboard.Tests.TransmissionModes
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
    }
}
