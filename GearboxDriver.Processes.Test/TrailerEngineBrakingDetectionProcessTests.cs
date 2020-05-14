using GearboxDriver.Gearshift;
using GearboxDriver.Hardware.ACL;
using GearboxDriver.PublishedLanguage.VehicleMotion;
using Moq;
using NUnit.Framework;

namespace GearboxDriver.Processes.Test
{
    public class SmoothBrakingWithTrailerAttachedTests
    {
        [Test]
        public void WhenVehicleTiltedDownHillCarIsMovingDownhill()
        {
            var serviceMock = new Mock<IGearshiftService>();
            var manager = new TrailerEngineBrakingDetectionProcess(serviceMock.Object);

            var @event = new VehicleTiltedDownhill();

            manager.ApplyEvent(@event);
        }

    }
}
