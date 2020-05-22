using GearboxDriver.Gearshift;
using Moq;
using NUnit.Framework;
using GearboxDriver.PublishedLanguage.VehicleMotion;
using GearboxDriver.PublishedLanguage.MDynamic;

namespace GearboxDriver.Processes.Test
{
    public class MDynamicSlippingDetectionProcessTests
    {
        [Test]
        public void WhenVehicleIsSlippingAbstainFromChangingGears()
        {
            var serviceMock = new Mock<IGearshiftService>();

            var processManager = new Processes.MDynamicSlippingDetectionProcess(serviceMock.Object);

            processManager.ApplyEvent(new MDynamicModeEntered());
            processManager.ApplyEvent(new VehicleStartedSlipping());

            serviceMock.Verify(x => x.AbstainFromChangingGears(), Times.Once);
        }

        [Test]
        public void WhenVehicleStoppedSlippingStopAbstainingFromChangingGears()
        {
            var serviceMock = new Mock<IGearshiftService>();

            var processManager = new Processes.MDynamicSlippingDetectionProcess(serviceMock.Object);

            processManager.ApplyEvent(new MDynamicModeEntered());
            processManager.ApplyEvent(new VehicleStartedSlipping());
            processManager.ApplyEvent(new VehicleStoppedSlipping());

            serviceMock.Verify(x => x.StopAbstainingFromChangingGears(), Times.Once);
        }
    }
}
