using GearboxDriver.PublishedLanguage.Gearbox;
using GearboxDriver.PublishedLanguage.Responsiveness;
using GearboxDriver.PublishedLanguage.SoundEffects;
using GearboxDriver.Seedwork;
using Moq;
using NUnit.Framework;

namespace GearboxDriver.Processes.Test
{
    public class ExhaustExplosionDetectionProcessTests
    {
        [Test]
        public void WhenGearDownShiftedAndAggressivenessLevelIsThirdSendExhaustExplosionOccuredEvent()
        {
            var eventBusMock = new Mock<IEventBus>();
            var process = new ExhaustExplosionDetectionProcess(eventBusMock.Object);

            process.ApplyEvent(new AggressivenessLevelSelected(AggressivenessLevel.Third));
            process.ApplyEvent(new GearChanged(new Gear(2), new Gear(3)));

            eventBusMock.Verify(x => x.SendEvent(It.IsAny<ExhaustExplosionOccured>()), Times.Once);
        }

        [Test]
        public void WhenGearUpshiftedAndAggressivenessLevelIsThirdDoNotSendExhaustExplosionOccuredEvent()
        {
            var eventBusMock = new Mock<IEventBus>();
            var process = new ExhaustExplosionDetectionProcess(eventBusMock.Object);

            process.ApplyEvent(new AggressivenessLevelSelected(AggressivenessLevel.Third));
            process.ApplyEvent(new GearChanged(new Gear(3), new Gear(2)));

            eventBusMock.Verify(x => x.SendEvent(It.IsAny<ExhaustExplosionOccured>()), Times.Never);
        }

        [Test]
        public void WhenGearIsDownShiftedAndAggressivenessLevelIsDifferentThanThirdDoNotSendExhaustExplosionOccuredEvent()
        {
            var eventBusMock = new Mock<IEventBus>();
            var process = new ExhaustExplosionDetectionProcess(eventBusMock.Object);

            process.ApplyEvent(new AggressivenessLevelSelected(AggressivenessLevel.Second));
            process.ApplyEvent(new GearChanged(new Gear(3), new Gear(2)));

            eventBusMock.Verify(x => x.SendEvent(It.IsAny<ExhaustExplosionOccured>()), Times.Never);
        }
    }
}
