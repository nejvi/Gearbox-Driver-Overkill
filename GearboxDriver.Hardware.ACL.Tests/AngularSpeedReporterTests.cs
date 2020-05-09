using GearboxDriver.Hardware.ACL.AngularSpeedReporting;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GearboxDriver.Hardware.ACL.Tests
{
    public class AngularSpeedReporterTests
    {
        private Mock<IEventBus> _eventBusMock;
        private Mock<IAngularSpeedProvider> _providerMock;
        private AngularSpeedReporter _reporter;

        [SetUp]
        public void SetUp()
        {
            _eventBusMock = new Mock<IEventBus>();
            _providerMock = new Mock<IAngularSpeedProvider>();
            _reporter = new AngularSpeedReporter(_eventBusMock.Object, _providerMock.Object);
        }

        [Test]
        public void WhenNoAngularSpeedChangedNoEventSent()
        {
            _eventBusMock.VerifyNoOtherCalls();
        }

        [Test]
        public void SendsAngularSpeedEventOnChange()
        {
            var angularSpeed = new AngularSpeed(2137d);
            _providerMock.Setup(x => x.GetCurrentAngularSpeed()).Returns(angularSpeed);

            _reporter.TryToReport();

            _eventBusMock.Verify(x => x.SendEvent(It.Is<AngularSpeedChanged>(y => y.AngularSpeed == angularSpeed)), Times.Once);
        }

        [Test]
        public void WhenValueDuplicatedSendsOnlyOneEvent()
        {
            var angularSpeed = new AngularSpeed(2137d);
            _providerMock.Setup(x => x.GetCurrentAngularSpeed()).Returns(angularSpeed);

            _reporter.TryToReport();
            _reporter.TryToReport();

            _eventBusMock.Verify(x => x.SendEvent(It.Is<AngularSpeedChanged>(y => y.AngularSpeed == angularSpeed)), Times.Once);
        }

        [Test]
        public void WhenTwoDifferentValuesSendsTwoEvents()
        {
            var firstAngularSpeed = new AngularSpeed(2137d);
            var secondAngularSpeed = new AngularSpeed(1728d);

            _providerMock.SetupSequence(x => x.GetCurrentAngularSpeed())
                .Returns(firstAngularSpeed)
                .Returns(secondAngularSpeed);

            _reporter.TryToReport();
            _reporter.TryToReport();

            _eventBusMock.Verify(x => x.SendEvent(It.Is<AngularSpeedChanged>(y => y.AngularSpeed == firstAngularSpeed)), Times.Once);
            _eventBusMock.Verify(x => x.SendEvent(It.Is<AngularSpeedChanged>(y => y.AngularSpeed == secondAngularSpeed)), Times.Once);
        }
    }
}
