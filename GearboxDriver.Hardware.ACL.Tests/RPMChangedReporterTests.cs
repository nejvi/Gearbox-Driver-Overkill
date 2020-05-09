using Moq;
using NUnit.Framework;

namespace GearboxDriver.Hardware.ACL.Tests
{
    public class RPMChangedReporterTests
    {
        private Mock<IEventBus> _eventBusMock;
        private RPMChangedReporter _reporter;
        
        [SetUp]
        public void SetUp()
        {
            _eventBusMock = new Mock<IEventBus>();
            _reporter = new RPMChangedReporter(_eventBusMock.Object);
        }

        [Test]
        public void WhenNoRpmChangedNoEventSent()
        {
            _eventBusMock.VerifyNoOtherCalls();
        }

        [Test]
        public void SendsRpmEventOnChange()
        {
            _reporter.ReadRpm(2137d);
            _eventBusMock.Verify(x => x.SendEvent(new RPMChanged(2137d)));
        }

        [Test]
        public void WhenValueDuplicatedSendsOnlyOneEvent()
        {
            _reporter.ReadRpm(2137d);
            _reporter.ReadRpm(2137d);

            _eventBusMock.Verify(x => x.SendEvent(new RPMChanged(2137d)));
        }

        [Test]
        public void WhenTwoDifferentValuesSendsTwoEvents()
        {
            _reporter.ReadRpm(2137d);
            _reporter.ReadRpm(1728d);

            _eventBusMock.Verify(x => x.SendEvent(new RPMChanged(2137d)));
            _eventBusMock.Verify(x => x.SendEvent(new RPMChanged(1728d)));
        }
    }
}