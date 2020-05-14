using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using GearboxDriver.Hardware.ACL.TiltPositionReporting;
using GearboxDriver.PublishedLanguage.VehicleMotion;
using GearboxDriver.Seedwork;

namespace GearboxDriver.Hardware.ACL.Tests
{
    public class TiltPositionReporterTests
    {
        private Mock<IEventBus> _eventBusMock;
        private Mock<ITiltPositionSensor> _providerMock;
        private TiltChangeReporter _reporter;

        [SetUp]
        public void SetUp()
        {
            _eventBusMock = new Mock<IEventBus>();
            _providerMock = new Mock<ITiltPositionSensor>();
            _reporter = new TiltChangeReporter(_eventBusMock.Object, _providerMock.Object);
        }

        [Test]
        public void WhenNoTiltPositionChangedNoEventSent()
        {
            _eventBusMock.VerifyNoOtherCalls();
        }

        [Test]
        public void SendsTiltPositionEventOnChange()
        {
            var tiltPosition = TiltPosition.Upwards;
            _providerMock.Setup(x => x.GetTiltPosition()).Returns(tiltPosition);

            _reporter.TryToReport();

            _eventBusMock.Verify(x => x.SendEvent(It.IsAny<VehicleTiltedUphill>()), Times.Once);
        }

        [Test]
        public void WhenValueDuplicatedSendsOnlyOneEvent()
        {
            var tiltPosition = TiltPosition.Upwards;
            _providerMock.Setup(x => x.GetTiltPosition()).Returns(tiltPosition);

            _reporter.TryToReport();
            _reporter.TryToReport();

            _eventBusMock.Verify(x => x.SendEvent(It.IsAny<VehicleTiltedUphill>()), Times.Once);
        }

        [Test]
        public void WhenTwoDifferentValuesSendsTwoEvents()
        {
            _providerMock.SetupSequence(x => x.GetTiltPosition())
                .Returns(TiltPosition.Downwards)
                .Returns(TiltPosition.Balanced);

            _reporter.TryToReport();
            _reporter.TryToReport();

            _eventBusMock.Verify(x => x.SendEvent(It.IsAny<VehicleTiledToStraightPosition>()), Times.Once);
            _eventBusMock.Verify(x => x.SendEvent(It.IsAny<VehicleTiltedDownhill>()), Times.Once);
        }

        [Test]
        public void WhenTiltPositionIsBalancedSendsEvent()
        {
            _providerMock.SetupSequence(x => x.GetTiltPosition())
                .Returns(TiltPosition.Downwards)
                .Returns(TiltPosition.Balanced);

            _reporter.TryToReport();
            _reporter.TryToReport();

            _eventBusMock.Verify(x => x.SendEvent(It.IsAny<VehicleTiledToStraightPosition>()), Times.Once);
        }

        [Test]
        public void WhenTiltPositionIsDownwardsSendsEvent()
        {
            var tiltPosition = TiltPosition.Downwards;
            _providerMock.Setup(x => x.GetTiltPosition()).Returns(tiltPosition);

            _reporter.TryToReport();

            _eventBusMock.Verify(x => x.SendEvent(It.IsAny<VehicleTiltedDownhill>()), Times.Once);
        }

        [Test]
        public void WhenTiltPositionIsUpwardsSendsEvent()
        {
            var tiltPosition = TiltPosition.Upwards;
            _providerMock.Setup(x => x.GetTiltPosition()).Returns(tiltPosition);

            _reporter.TryToReport();

            _eventBusMock.Verify(x => x.SendEvent(It.IsAny<VehicleTiltedUphill>()), Times.Once);
        }
    }
}
