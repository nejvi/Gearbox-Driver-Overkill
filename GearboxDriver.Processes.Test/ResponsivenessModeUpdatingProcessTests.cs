using GearboxDriver.Cabin.Responsiveness;
using GearboxDriver.Gearshift;
using Moq;
using NUnit.Framework;

namespace GearboxDriver.Processes.Test
{
    public class ResponsivenessModeUpdatingProcessTests
    {
        [Test]
        public void WhenComfortModeEnteredAndFirstAggressivenessLevelSelectedEventOccursSendsAdequateDemand()
        {
            var serviceMock = new Mock<IGearshiftService>();
            var characteristics = new Characteristics();
            var processManager = new ResponsivenessModeUpdatingProcess(serviceMock.Object, characteristics);

            processManager.ApplyEvent(new ComfortModeEntered());
            processManager.ApplyEvent(new AggressivenessLevelSelected(AggressivenessLevel.First));

            serviceMock.Verify(x => x.KeepFollowingRpm(characteristics.GetRangeFor(ResponsivenessMode.Comfort, AggressivenessLevel.First)), Times.Exactly(2));
        }

        [Test]
        public void WhenComfortModeEnteredAndAggressivenessLevelSelectedIsSecondEventOccuredSendsAdequateDemand()
        {
            var serviceMock = new Mock<IGearshiftService>();
            var characteristics = new Characteristics();
            var processManager = new ResponsivenessModeUpdatingProcess(serviceMock.Object, characteristics);

            processManager.ApplyEvent(new ComfortModeEntered());
            processManager.ApplyEvent(new AggressivenessLevelSelected(AggressivenessLevel.Second));

            serviceMock.Verify(x => x.KeepFollowingRpm(characteristics.GetRangeFor(ResponsivenessMode.Comfort, AggressivenessLevel.First)), Times.Once);
            serviceMock.Verify(x => x.KeepFollowingRpm(characteristics.GetRangeFor(ResponsivenessMode.Comfort, AggressivenessLevel.Second)), Times.Once);
        }

        [Test]
        public void WhenComfortModeEnteredAndAggressivenessLevelSelectedIsThirdEventOccuredSendsAdequateDemand()
        {
            var serviceMock = new Mock<IGearshiftService>();
            var characteristics = new Characteristics();
            var processManager = new ResponsivenessModeUpdatingProcess(serviceMock.Object, characteristics);

            processManager.ApplyEvent(new ComfortModeEntered());
            processManager.ApplyEvent(new AggressivenessLevelSelected(AggressivenessLevel.Third));

            serviceMock.Verify(x => x.KeepFollowingRpm(characteristics.GetRangeFor(ResponsivenessMode.Comfort, AggressivenessLevel.Third)), Times.Once);
        }

        [Test]
        public void WhenEconomicModeEnteredAndAggressivenessLevelSelectedIsFirstEventOccuredSendsAdequateDemand()
        {
            var serviceMock = new Mock<IGearshiftService>();
            var characteristics = new Characteristics();
            var processManager = new ResponsivenessModeUpdatingProcess(serviceMock.Object, characteristics);

            processManager.ApplyEvent(new EconomicModeEntered());
            processManager.ApplyEvent(new AggressivenessLevelSelected(AggressivenessLevel.First));

            serviceMock.Verify(x => x.KeepFollowingRpm(characteristics.GetRangeFor(ResponsivenessMode.Economic, AggressivenessLevel.First)), Times.Exactly(2));
        }

        [Test]
        public void WhenEconomicModeEnteredAndAggressivenessLevelSelectedIsSecondEventOccuredSendsAdequateDemand()
        {
            var serviceMock = new Mock<IGearshiftService>();
            var characteristics = new Characteristics();
            var processManager = new ResponsivenessModeUpdatingProcess(serviceMock.Object, characteristics);

            processManager.ApplyEvent(new EconomicModeEntered());
            processManager.ApplyEvent(new AggressivenessLevelSelected(AggressivenessLevel.Second));

            serviceMock.Verify(x => x.KeepFollowingRpm(characteristics.GetRangeFor(ResponsivenessMode.Economic, AggressivenessLevel.Second)), Times.Once);
        }

        [Test]
        public void WhenEconomicModeEnteredAndAggressivenessLevelSelectedIsThirdEventOccuredSendsAdequateDemand()
        {
            var serviceMock = new Mock<IGearshiftService>();
            var characteristics = new Characteristics();
            var processManager = new ResponsivenessModeUpdatingProcess(serviceMock.Object, characteristics);

            processManager.ApplyEvent(new EconomicModeEntered());
            processManager.ApplyEvent(new AggressivenessLevelSelected(AggressivenessLevel.Third));

            serviceMock.Verify(x => x.KeepFollowingRpm(characteristics.GetRangeFor(ResponsivenessMode.Economic, AggressivenessLevel.Third)), Times.Once);
        }

        [Test]
        public void WhenSportModeEnteredAndAggressivenessLevelSelectedIsFirstEventOccuredSendsAdequateDemand()
        {
            var serviceMock = new Mock<IGearshiftService>();
            var characteristics = new Characteristics();
            var processManager = new ResponsivenessModeUpdatingProcess(serviceMock.Object, characteristics);

            processManager.ApplyEvent(new SportModeEntered());
            processManager.ApplyEvent(new AggressivenessLevelSelected(AggressivenessLevel.First));

            serviceMock.Verify(x => x.KeepFollowingRpm(characteristics.GetRangeFor(ResponsivenessMode.Sport, AggressivenessLevel.First)), Times.Exactly(2));
        }

        [Test]
        public void WhenSportModeEnteredAndAggressivenessLevelSelectedIsSecondEventOccuredSendsAdequateDemand()
        {
            var serviceMock = new Mock<IGearshiftService>();
            var characteristics = new Characteristics();
            var processManager = new ResponsivenessModeUpdatingProcess(serviceMock.Object, characteristics);

            processManager.ApplyEvent(new SportModeEntered());
            processManager.ApplyEvent(new AggressivenessLevelSelected(AggressivenessLevel.Second));

            serviceMock.Verify(x => x.KeepFollowingRpm(characteristics.GetRangeFor(ResponsivenessMode.Sport, AggressivenessLevel.Second)), Times.Once);
        }

        [Test]
        public void WhenSportModeEnteredAndAggressivenessLevelSelectedIsThirdEventOccuredSendsAdequateDemand()
        {
            var serviceMock = new Mock<IGearshiftService>();
            var characteristics = new Characteristics();
            var processManager = new ResponsivenessModeUpdatingProcess(serviceMock.Object, characteristics);

            processManager.ApplyEvent(new SportModeEntered());
            processManager.ApplyEvent(new AggressivenessLevelSelected(AggressivenessLevel.Third));

            serviceMock.Verify(x => x.KeepFollowingRpm(characteristics.GetRangeFor(ResponsivenessMode.Sport, AggressivenessLevel.Third)), Times.Once);
        }
    }
}
