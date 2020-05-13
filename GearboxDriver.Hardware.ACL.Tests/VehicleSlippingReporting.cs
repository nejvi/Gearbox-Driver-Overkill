using GearboxDriver.Hardware.ACL.VehicleSlippingReporting;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using GearboxDriver.Seedwork;

namespace GearboxDriver.Hardware.ACL.Tests
{
    public class VehicleSlippingReporting
    {
        private Mock<IEventBus> _eventBusMock;
        private Mock<ISlippingProvider> _providerMock;
        private SlippingReporter _reporter;

        [SetUp]
        public void SetUp()
        {
            _eventBusMock = new Mock<IEventBus>();
            _providerMock = new Mock<ISlippingProvider>();
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
            _providerMock.Setup(x => x.IsCurrentlySlipping()).Returns(false);

            _reporter.TryToReport();

            _eventBusMock.Verify(x => x.SendEvent(It.IsAny<VehicleStoppedSlipping>()), Times.Once);
        }

    }
}
