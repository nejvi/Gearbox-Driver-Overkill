using GearboxDriver.Hardware.ACL.LightsReporting;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GearboxDriver.Hardware.ACL.Tests
{
    public class LightsPositionReporterTests
    {
        private Mock<IEventBus> _eventBusMock;
        private Mock<ILightsPositionProvider> _providerMock;
        private LightsPositionReporter _reporter;

        [SetUp]
        public void SetUp()
        {
            _eventBusMock = new Mock<IEventBus>();
            _providerMock = new Mock<ILightsPositionProvider>();
            _reporter = new LightsPositionReporter(_eventBusMock.Object, _providerMock.Object);
        }

        [Test]
        public void WhenNoLightsPositionChangedNoEventSent()
        {
            _eventBusMock.VerifyNoOtherCalls();
        }

        [Test]
        public void SendsLightsPositionEventOnChange()
        {
            var lightPosition = new LightsPosition(4);
            _providerMock.Setup(x => x.GetCurrentLights()).Returns(lightPosition);

            _reporter.TryToReport();

            _eventBusMock.Verify(x => x.SendEvent(It.Is<LightsPositionChanged>(y => y.LightsPosition == lightPosition)), Times.Once);
        }

        [Test]
        public void WhenValueDuplicatedSendsOnlyOneEvent()
        {
            var lightPosition = new LightsPosition(4);
            _providerMock.Setup(x => x.GetCurrentLights()).Returns(lightPosition);

            _reporter.TryToReport();
            _reporter.TryToReport();

            _eventBusMock.Verify(x => x.SendEvent(It.Is<LightsPositionChanged>(y => y.LightsPosition == lightPosition)), Times.Once);
        }

        [Test]
        public void WhenTwoDifferentValuesSendsTwoEvents()
        {
            var firstLightPosition = new LightsPosition(4);
            var secondLightPosition = new LightsPosition(7);

            _providerMock.SetupSequence(x => x.GetCurrentLights())
                .Returns(firstLightPosition)
                .Returns(secondLightPosition);

            _reporter.TryToReport();
            _reporter.TryToReport();

            _eventBusMock.Verify(x => x.SendEvent(It.Is<LightsPositionChanged>(y => y.LightsPosition == firstLightPosition)), Times.Once);
            _eventBusMock.Verify(x => x.SendEvent(It.Is<LightsPositionChanged>(y => y.LightsPosition == secondLightPosition)), Times.Once);
        }
    }
}
