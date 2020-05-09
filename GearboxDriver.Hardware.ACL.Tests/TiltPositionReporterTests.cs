using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using GearboxDriver.Hardware.ACL.TiltPositionReporting;

namespace GearboxDriver.Hardware.ACL.Tests
{
    public class TiltPositionReporterTests
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
        public void WhenNoTiltPositionChangedNoEventSent()
        {
            _eventBusMock.VerifyNoOtherCalls();
        }

        [Test]
        public void SendsTiltPositionEventOnChange()
        {
            var tiltPosition = TiltPosition.Balanced;
            _providerMock.Setup(x => x.GetTiltPosition()).Returns(tiltPosition);

            _reporter.TryToReport();

            _eventBusMock.Verify(x => x.SendEvent(It.IsAny<VehicleTiledToStraightPosition>()), Times.Once);
        }

        [Test]
        public void WhenValueDuplicatedSendsOnlyOneEvent()
        {
            var tiltPosition = TiltPosition.Balanced;
            _providerMock.Setup(x => x.GetTiltPosition()).Returns(tiltPosition);

            _reporter.TryToReport();
            _reporter.TryToReport();

            _eventBusMock.Verify(x => x.SendEvent(It.IsAny<VehicleTiledToStraightPosition>()), Times.Once);
        }

        [Test]
        public void WhenTwoDifferentValuesSendsTwoEvents()
        {
            var firstTiltPosition = TiltPosition.Balanced;
            var secondTiltPosition = TiltPosition.Downwards;

            _providerMock.SetupSequence(x => x.GetTiltPosition())
                .Returns(firstTiltPosition)
                .Returns(secondTiltPosition);

            _reporter.TryToReport();
            _reporter.TryToReport();

            _eventBusMock.Verify(x => x.SendEvent(It.IsAny<VehicleTiledToStraightPosition>()), Times.Once);
            _eventBusMock.Verify(x => x.SendEvent(It.IsAny<VehicleTiltedDownhill>()), Times.Once);
        }

        [Test]
        public void WhenTiltPositionIsBalancedSendsEvent()
        {
            var tiltPosition = TiltPosition.Balanced;
            _providerMock.Setup(x => x.GetTiltPosition()).Returns(tiltPosition);

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
