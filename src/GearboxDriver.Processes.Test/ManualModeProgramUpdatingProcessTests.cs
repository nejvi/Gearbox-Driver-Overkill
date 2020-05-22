using GearboxDriver.Gearshift;
using GearboxDriver.PublishedLanguage.Gearbox;
using GearboxDriver.PublishedLanguage.ManualGearshifting;
using Moq;
using NUnit.Framework;

namespace GearboxDriver.Processes.Test
{
    public class ManualModeProgramUpdatingProcessTests
    {
        [Test]
        public void WhenManualGearshiftingModeExitedStopTargetingAnyGear()
        {
            var serviceMock = new Mock<IGearshiftService>();
            var process = new ManualModeProgramUpdatingProcess(serviceMock.Object);

            process.ApplyEvent(new GearChanged(new Gear(2), new Gear(3)));
            process.ApplyEvent(new ManualGearshiftingModeEntered());
            process.ApplyEvent(new ManualGearshiftingModeExited());

            serviceMock.Verify(x => x.StopTargetingAnyGear(), Times.Once);
        }

        [Test]
        public void WhenManualGearshiftingModeEnteredStartTargetingGear()
        {
            var serviceMock = new Mock<IGearshiftService>();
            var process = new ManualModeProgramUpdatingProcess(serviceMock.Object);

            process.ApplyEvent(new GearChanged(new Gear(2), new Gear(3)));
            process.ApplyEvent(new ManualGearshiftingModeEntered());

            serviceMock.Verify(x => x.TargetGear(new Gear(2)), Times.Once);
        }

        [Test]
        public void WhenGearUpshiftedManuallyGearIsUpshifted()
        {
            var serviceMock = new Mock<IGearshiftService>();
            var process = new ManualModeProgramUpdatingProcess(serviceMock.Object);

            process.ApplyEvent(new GearChanged(new Gear(2), new Gear(3)));
            process.ApplyEvent(new ManualGearshiftingModeEntered());
            process.ApplyEvent(new GearUpshiftedManually());

            serviceMock.Verify(x => x.TargetGear(new Gear(3)));
        }

        [Test]
        public void WhenGearDownshiftedManuallyGearIsDownshifted()
        {
            var serviceMock = new Mock<IGearshiftService>();
            var process = new ManualModeProgramUpdatingProcess(serviceMock.Object);

            process.ApplyEvent(new GearChanged(new Gear(2), new Gear(3)));
            process.ApplyEvent(new ManualGearshiftingModeEntered());
            process.ApplyEvent(new GearUpshiftedManually());

            serviceMock.Verify(x => x.TargetGear(new Gear(2)));
        }
    }
}
