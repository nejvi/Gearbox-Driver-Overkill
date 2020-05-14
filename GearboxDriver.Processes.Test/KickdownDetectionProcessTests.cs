using GearboxDriver.Gearshift;
using GearboxDriver.PublishedLanguage.Gearbox;
using GearboxDriver.PublishedLanguage.Pedals;
using GearboxDriver.PublishedLanguage.Responsiveness;
using Moq;
using NUnit.Framework;

namespace GearboxDriver.Processes.Test
{
    public class KickdownDetectionProcessTests
    {
        [Test]
        public void WhenComfortModeEnteredAndGasPressureChangedToSingularKickdownAndNewRpmChangedToSingularTargetGearIsDownshiftedByOne()
        {
            var characteristics = new KickdownCharacteristics();
            var serviceMock = new Mock<IGearshiftService>();
            var process = new KickdownDetectionProcess(characteristics, serviceMock.Object);;

            process.ApplyEvent(new GearChanged(new Gear(3), new Gear(2)));
            process.ApplyEvent(new RpmChanged(new Rpm(4500)));
            process.ApplyEvent(new ComfortModeEntered());
            process.ApplyEvent(new GasPressureChanged(new PedalPressure(0.75)));

            serviceMock.Verify(x => x.TargetGear(new Gear(3).DownshiftedBy(new Gear(1))), Times.Once);
        }        

        [Test]
        public void WhenSportModeEnteredAndGasPressureChangedToSingularKickdownAndNewRpmChangedToSingularTargetGearIsDownshiftedByOne()
        {
            var characteristics = new KickdownCharacteristics();
            var serviceMock = new Mock<IGearshiftService>();
            var process = new KickdownDetectionProcess(characteristics, serviceMock.Object);;

            process.ApplyEvent(new GearChanged(new Gear(3), new Gear(2)));
            process.ApplyEvent(new RpmChanged(new Rpm(5000)));
            process.ApplyEvent(new SportModeEntered());
            process.ApplyEvent(new GasPressureChanged(new PedalPressure(0.75)));

            serviceMock.Verify(x => x.TargetGear(new Gear(3).DownshiftedBy(new Gear(1))), Times.Once);
        }        

        [Test]
        public void WhenSportModeEnteredAndGasPressureChangedToSingularKickdownAndNewRpmChangedToSingularTargetGearIsDownshiftedByTwo()
        {
            var characteristics = new KickdownCharacteristics();
            var serviceMock = new Mock<IGearshiftService>();
            var process = new KickdownDetectionProcess(characteristics, serviceMock.Object);;

            process.ApplyEvent(new GearChanged(new Gear(3), new Gear(2)));
            process.ApplyEvent(new RpmChanged(new Rpm(5000)));
            process.ApplyEvent(new SportModeEntered());
            process.ApplyEvent(new GasPressureChanged(new PedalPressure(0.9)));

            serviceMock.Verify(x => x.TargetGear(new Gear(3).DownshiftedBy(new Gear(2))), Times.Once);
        }
    }
}
