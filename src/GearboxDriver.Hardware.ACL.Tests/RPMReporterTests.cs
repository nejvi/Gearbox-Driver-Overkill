using GearboxDriver.Hardware.ACL.RpmReporting;
using GearboxDriver.PublishedLanguage.Gearbox;
using GearboxDriver.Seedwork;
using Moq;
using NUnit.Framework;

namespace GearboxDriver.Hardware.ACL.Tests
{
    public class RPMReporterTests
    {
        private Mock<IEventBus> _eventBusMock;
        private Mock<IRpmSensor> _providerMock;
        private RpmReporter _reporter;
        
        [SetUp]
        public void SetUp()
        {
            _eventBusMock = new Mock<IEventBus>();
            _providerMock = new Mock<IRpmSensor>();
            _reporter = new RpmReporter(_eventBusMock.Object, _providerMock.Object);
        }

        [Test]
        public void WhenNoRpmChangedNoEventSent()
        {
            _eventBusMock.VerifyNoOtherCalls();
        }

        [Test]
        public void SendsRpmEventOnChange()
        {
            var rpm = new Rpm(2137d);
            _providerMock.Setup(x => x.GetCurrentRpm()).Returns(rpm);

            _reporter.TryToReport();

            _eventBusMock.Verify(x => x.SendEvent(It.Is<RpmChanged>(y => y.NewRpm == rpm)), Times.Once);
        }

        [Test]
        public void WhenValueDuplicatedSendsOnlyOneEvent()
        {
            var rpm = new Rpm(2137d);
            _providerMock.Setup(x => x.GetCurrentRpm()).Returns(rpm);

            _reporter.TryToReport();
            _reporter.TryToReport();

            _eventBusMock.Verify(x => x.SendEvent(It.Is<RpmChanged>(y => y.NewRpm == rpm)), Times.Once);
        }

        [Test]
        public void WhenTwoDifferentValuesSendsTwoEvents()
        {
            var firstRpm = new Rpm(2137d);
            var secondRpm = new Rpm(1728d);

            _providerMock.SetupSequence(x => x.GetCurrentRpm())
                .Returns(firstRpm)
                .Returns(secondRpm);

            _reporter.TryToReport();
            _reporter.TryToReport();

            _eventBusMock.Verify(x => x.SendEvent(It.Is<RpmChanged>(y => y.NewRpm == firstRpm)), Times.Once);
            _eventBusMock.Verify(x => x.SendEvent(It.Is<RpmChanged>(y => y.NewRpm == secondRpm)), Times.Once);
        }
    }
}