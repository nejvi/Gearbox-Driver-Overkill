using GearboxDriver.Cabin.Responsiveness;
using GearboxDriver.Gearshift;
using Moq;
using NUnit.Framework;

namespace GearboxDriver.Processes.Test
{
    public class RpmBasedByModesTests
    {
        [Test]
        public void WhenComfortModeEnteredAndAggressivenessLevelSelectedIsFirstEventOccuredSendsProperRange()
        {
            var serviceMock = new Mock<IGearshiftService>();
            var characteristics = new Characteristics();
            var processManager = new RpmBasedByModes(serviceMock.Object, characteristics);

            processManager.ApplyEvent(new ComfortModeEntered());
            processManager.ApplyEvent(new AggressivenessLevelSelected(AggressivenessLevel.First));

            serviceMock.Verify(x => x.KeepFollowingRpm(characteristics.GetRangeFor(ResponsivenessMode.Comfort, AggressivenessLevel.First)), Times.Exactly(2));
        }

        [Test]
        public void WhenComfortModeEnteredAndAggressivenessLevelSelectedIsSecondEventOccuredSendsProperRange()
        {
            var serviceMock = new Mock<IGearshiftService>();
            var characteristics = new Characteristics();
            var processManager = new RpmBasedByModes(serviceMock.Object, characteristics);

            processManager.ApplyEvent(new ComfortModeEntered());
            processManager.ApplyEvent(new AggressivenessLevelSelected(AggressivenessLevel.Second));

            serviceMock.Verify(x => x.KeepFollowingRpm(characteristics.GetRangeFor(ResponsivenessMode.Comfort, AggressivenessLevel.First)), Times.Once);
            serviceMock.Verify(x => x.KeepFollowingRpm(characteristics.GetRangeFor(ResponsivenessMode.Comfort, AggressivenessLevel.Second)), Times.Once);
        }

        [Test]
        public void WhenComfortModeEnteredAndAggressivenessLevelSelectedIsThirdEventOccuredSendsProperRange()
        {
            var serviceMock = new Mock<IGearshiftService>();
            var characteristics = new Characteristics();
            var processManager = new RpmBasedByModes(serviceMock.Object, characteristics);

            processManager.ApplyEvent(new ComfortModeEntered());
            processManager.ApplyEvent(new AggressivenessLevelSelected(AggressivenessLevel.Third));

            serviceMock.Verify(x => x.KeepFollowingRpm(characteristics.GetRangeFor(ResponsivenessMode.Comfort, AggressivenessLevel.Third)), Times.Once);
        }

        [Test]
        public void WhenEconomicModeEnteredAndAggressivenessLevelSelectedIsFirstEventOccuredSendsProperRange()
        {
            var serviceMock = new Mock<IGearshiftService>();
            var characteristics = new Characteristics();
            var processManager = new RpmBasedByModes(serviceMock.Object, characteristics);

            processManager.ApplyEvent(new EconomicModeEntered());
            processManager.ApplyEvent(new AggressivenessLevelSelected(AggressivenessLevel.First));

            serviceMock.Verify(x => x.KeepFollowingRpm(characteristics.GetRangeFor(ResponsivenessMode.Economic, AggressivenessLevel.First)), Times.Exactly(2));
        }

        [Test]
        public void WhenEconomicModeEnteredAndAggressivenessLevelSelectedIsSecondEventOccuredSendsProperRange()
        {
            var serviceMock = new Mock<IGearshiftService>();
            var characteristics = new Characteristics();
            var processManager = new RpmBasedByModes(serviceMock.Object, characteristics);

            processManager.ApplyEvent(new EconomicModeEntered());
            processManager.ApplyEvent(new AggressivenessLevelSelected(AggressivenessLevel.Second));

            serviceMock.Verify(x => x.KeepFollowingRpm(characteristics.GetRangeFor(ResponsivenessMode.Economic, AggressivenessLevel.Second)), Times.Once);
        }

        [Test]
        public void WhenEconomicModeEnteredAndAggressivenessLevelSelectedIsThirdEventOccuredSendsProperRange()
        {
            var serviceMock = new Mock<IGearshiftService>();
            var characteristics = new Characteristics();
            var processManager = new RpmBasedByModes(serviceMock.Object, characteristics);

            processManager.ApplyEvent(new EconomicModeEntered());
            processManager.ApplyEvent(new AggressivenessLevelSelected(AggressivenessLevel.Third));

            serviceMock.Verify(x => x.KeepFollowingRpm(characteristics.GetRangeFor(ResponsivenessMode.Economic, AggressivenessLevel.Third)), Times.Once);
        }

        [Test]
        public void WhenSportModeEnteredAndAggressivenessLevelSelectedIsFirstEventOccuredSendsProperRange()
        {
            var serviceMock = new Mock<IGearshiftService>();
            var characteristics = new Characteristics();
            var processManager = new RpmBasedByModes(serviceMock.Object, characteristics);

            processManager.ApplyEvent(new SportModeEntered());
            processManager.ApplyEvent(new AggressivenessLevelSelected(AggressivenessLevel.First));

            serviceMock.Verify(x => x.KeepFollowingRpm(characteristics.GetRangeFor(ResponsivenessMode.Sport, AggressivenessLevel.First)), Times.Exactly(2));
        }

        [Test]
        public void WhenSportModeEnteredAndAggressivenessLevelSelectedIsSecondEventOccuredSendsProperRange()
        {
            var serviceMock = new Mock<IGearshiftService>();
            var characteristics = new Characteristics();
            var processManager = new RpmBasedByModes(serviceMock.Object, characteristics);

            processManager.ApplyEvent(new SportModeEntered());
            processManager.ApplyEvent(new AggressivenessLevelSelected(AggressivenessLevel.Second));

            serviceMock.Verify(x => x.KeepFollowingRpm(characteristics.GetRangeFor(ResponsivenessMode.Sport, AggressivenessLevel.Second)), Times.Once);
        }

        [Test]
        public void WhenSportModeEnteredAndAggressivenessLevelSelectedIsThirdEventOccuredSendsProperRange()
        {
            var serviceMock = new Mock<IGearshiftService>();
            var characteristics = new Characteristics();
            var processManager = new RpmBasedByModes(serviceMock.Object, characteristics);

            processManager.ApplyEvent(new SportModeEntered());
            processManager.ApplyEvent(new AggressivenessLevelSelected(AggressivenessLevel.Third));

            serviceMock.Verify(x => x.KeepFollowingRpm(characteristics.GetRangeFor(ResponsivenessMode.Sport, AggressivenessLevel.Third)), Times.Once);
        }
    }
}
