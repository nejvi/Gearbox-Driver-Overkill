﻿using GearboxDriver.Hardware.ACL.VehicleSlippingReporting;
using Moq;
using NUnit.Framework;
using GearboxDriver.PublishedLanguage.VehicleMotion;
using GearboxDriver.Seedwork;

namespace GearboxDriver.Hardware.ACL.Tests
{
    public class VehicleSlippingReporting
    {
        private Mock<IEventBus> _eventBusMock;
        private Mock<ISlippingSensor> _providerMock;
        private SlippingReporter _reporter;

        [SetUp]
        public void SetUp()
        {
            _eventBusMock = new Mock<IEventBus>();
            _providerMock = new Mock<ISlippingSensor>();
            _reporter = new SlippingReporter(_eventBusMock.Object, _providerMock.Object);
        }

        [Test]
        public void WhenNoAngularSpeedChangedNoEventSent()
        {
            _eventBusMock.VerifyNoOtherCalls();
        }

        [Test]
        public void SendsAngularSpeedEventOnChange()
        {
            _providerMock.Setup(x => x.IsCurrentlySlipping()).Returns(true);

            _reporter.TryToReport();

            _eventBusMock.Verify(x => x.SendEvent(It.IsAny<VehicleStartedSlipping>()), Times.Once);
        }

        [Test]
        public void WhenValueDuplicatedSendsOnlyOneEvent()
        {
            _providerMock.Setup(x => x.IsCurrentlySlipping()).Returns(true);

            _reporter.TryToReport();
            _reporter.TryToReport();

            _eventBusMock.Verify(x => x.SendEvent(It.IsAny<VehicleStartedSlipping>()), Times.Once);
        }

        [Test]
        public void WhenTwoDifferentValuesSendsTwoEvents()
        {
            _providerMock.SetupSequence(x => x.IsCurrentlySlipping())
                .Returns(true)
                .Returns(false);

            _reporter.TryToReport();
            _reporter.TryToReport();

            _eventBusMock.Verify(x => x.SendEvent(It.IsAny<VehicleStartedSlipping>()), Times.Once);
            _eventBusMock.Verify(x => x.SendEvent(It.IsAny<VehicleStoppedSlipping>()), Times.Once);
        }

        [Test]
        public void WhenVehicleCurrentlySlippingSendsEvent()
        {
            _providerMock.Setup(x => x.IsCurrentlySlipping()).Returns(true);

            _reporter.TryToReport();

            _eventBusMock.Verify(x => x.SendEvent(It.IsAny<VehicleStartedSlipping>()), Times.Once);
        }

        [Test]
        public void WhenVehicleIsNotCurrentlySlippingSendsEvent()
        {
            _providerMock.SetupSequence(x => x.IsCurrentlySlipping())
                .Returns(true)
                .Returns(false);

            _reporter.TryToReport();
            _reporter.TryToReport();

            _eventBusMock.Verify(x => x.SendEvent(It.IsAny<VehicleStoppedSlipping>()), Times.Once);
        }

    }
}
