using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using GearboxDriver.Hardware.ACL.LightPositionReporting;

namespace GearboxDriver.Hardware.ACL.Tests
{
    public class LightsPositionReporterTests
    {
        private Mock<IEventBus> _eventBusMock;
        private Mock<ITiltPositionProvider> _providerMock;
        private TiltChangeReporter _reporter;

        [SetUp]
        public void SetUp()
        {
            _eventBusMock = new Mock<IEventBus>();
            _providerMock = new Mock<ITiltPositionProvider>();
            _reporter = new TiltChangeReporter(_eventBusMock.Object, _providerMock.Object);
        }

        [Test]
        public void WhenNoLightsPositionChangedNoEventSent()
        {
            _eventBusMock.VerifyNoOtherCalls();
        }

        [Test]
        public void SendsLightsPositionEventOnChange()
        {
            var lightPosition = new VehicleTiltPosition(4);
            _providerMock.Setup(x => x.GetTiltPosition()).Returns(lightPosition);

            _reporter.TryToReport();

            _eventBusMock.Verify(x => x.SendEvent(It.Is<LightsPositionChanged>(y => y.LightsPosition == lightPosition)), Times.Once);
        }

        [Test]
        public void WhenValueDuplicatedSendsOnlyOneEvent()
        {
            var lightPosition = new VehicleTiltPosition(4);
            _providerMock.Setup(x => x.GetTiltPosition()).Returns(lightPosition);

            _reporter.TryToReport();
            _reporter.TryToReport();

            _eventBusMock.Verify(x => x.SendEvent(It.Is<LightsPositionChanged>(y => y.LightsPosition == lightPosition)), Times.Once);
        }

        [Test]
        public void WhenTwoDifferentValuesSendsTwoEvents()
        {
            var firstLightPosition = new VehicleTiltPosition(4);
            var secondLightPosition = new VehicleTiltPosition(7);

            _providerMock.SetupSequence(x => x.GetTiltPosition())
                .Returns(firstLightPosition)
                .Returns(secondLightPosition);

            _reporter.TryToReport();
            _reporter.TryToReport();

            _eventBusMock.Verify(x => x.SendEvent(It.Is<LightsPositionChanged>(y => y.LightsPosition == firstLightPosition)), Times.Once);
            _eventBusMock.Verify(x => x.SendEvent(It.Is<LightsPositionChanged>(y => y.LightsPosition == secondLightPosition)), Times.Once);
        }
    }
}
