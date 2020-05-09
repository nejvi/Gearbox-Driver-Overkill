using GearboxDriver.Hardware.ACL.RPMReporting;
using Moq;
using NUnit.Framework;

namespace GearboxDriver.Hardware.ACL.Tests
{
    public class RPMReporterTests
    {
        private Mock<IEventBus> _eventBusMock;
        private Mock<IRPMProvider> _providerMock;
        private RPMReporter _reporter;
        
        [SetUp]
        public void SetUp()
        {
            _eventBusMock = new Mock<IEventBus>();
            _providerMock = new Mock<IRPMProvider>();
            _reporter = new RPMReporter(_eventBusMock.Object, _providerMock.Object);
        }

        [Test]
        public void WhenNoRpmChangedNoEventSent()
        {
            _eventBusMock.VerifyNoOtherCalls();
        }

        [Test]
        public void SendsRpmEventOnChange()
        {
            var rpm = new RPM(2137d);
            _providerMock.Setup(x => x.GetCurrentRpm()).Returns(rpm);

            _reporter.TryToReport();

            _eventBusMock.Verify(x => x.SendEvent(It.Is<RPMChanged>(y => y.Rpm == rpm)), Times.Once);
        }

        [Test]
        public void WhenValueDuplicatedSendsOnlyOneEvent()
        {
            var rpm = new RPM(2137d);
            _providerMock.Setup(x => x.GetCurrentRpm()).Returns(rpm);

            _reporter.TryToReport();
            _reporter.TryToReport();

            _eventBusMock.Verify(x => x.SendEvent(It.Is<RPMChanged>(y => y.Rpm == rpm)), Times.Once);
        }

        [Test]
        public void WhenTwoDifferentValuesSendsTwoEvents()
        {
            var firstRpm = new RPM(2137d);
            var secondRpm = new RPM(1728d);

            _providerMock.SetupSequence(x => x.GetCurrentRpm())
                .Returns(firstRpm)
                .Returns(secondRpm);

            _reporter.TryToReport();
            _reporter.TryToReport();

            _eventBusMock.Verify(x => x.SendEvent(It.Is<RPMChanged>(y => y.Rpm == firstRpm)), Times.Once);
            _eventBusMock.Verify(x => x.SendEvent(It.Is<RPMChanged>(y => y.Rpm == secondRpm)), Times.Once);
        }
    }
}