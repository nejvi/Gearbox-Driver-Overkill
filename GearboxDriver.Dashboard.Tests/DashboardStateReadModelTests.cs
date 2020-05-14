using GearboxDriver.Hardware.ACL;
using GearboxDriver.PublishedLanguage.Gearbox;
using GearboxDriver.PublishedLanguage.ManualGearshifting;
using GearboxDriver.PublishedLanguage.Responsiveness;
using GearboxDriver.PublishedLanguage.Transmission;
using NUnit.Framework;

namespace GearboxDriver.CabinControls.Tests
{
    public class DashboardStateReadModelTests
    {
        [Test]
        public void WhenParkModeEnteredEventOccuredCurrentTransmissionModeChangesToPark()
        {
            var dashboardState = new DashboardStateReadModel();
            var parkModeEntered = new ParkModeEntered();

            dashboardState.Apply(parkModeEntered);

            Assert.AreEqual(dashboardState.CurrentTransmissionMode, "PARK");
        }

        [Test]
        public void WhenReverseModeEnteredEventOccuredCurrentTransmissionModeChangesToReverse()
        {
            var dashboardState = new DashboardStateReadModel();
            var reverseModeEntered = new ReverseModeEntered();

            dashboardState.Apply(reverseModeEntered);

            Assert.AreEqual(dashboardState.CurrentTransmissionMode, "REVERSE");
        }

        [Test]
        public void WhenNeutralModeEnteredEventOccuredCurrentTransmissionModeChangesToNeutral()
        {
            var dashboardState = new DashboardStateReadModel();
            var neutralModeEntered = new NeutralModeEntered();

            dashboardState.Apply(neutralModeEntered);

            Assert.AreEqual(dashboardState.CurrentTransmissionMode, "NEUTRAL");
        }

        [Test]
        public void WhenDriveModeEnteredEventOccuredCurrentTransmissionModeChangesToDrive()
        {
            var dashboardState = new DashboardStateReadModel();
            var driveModeEntered = new DriveModeEntered();

            dashboardState.Apply(driveModeEntered);

            Assert.AreEqual(dashboardState.CurrentTransmissionMode, "DRIVE");
        }

        [Test]
        public void WhenComfortModeEnteredEventOccuredCurrentResponsivenessModeChangesToComfort()
        {
            var dashboardState = new DashboardStateReadModel();
            var comfortModeEntered = new ComfortModeEntered();

            dashboardState.Apply(comfortModeEntered);

            Assert.AreEqual(dashboardState.CurrentResponsivenessMode, "COMFORT");
        }

        [Test]
        public void WhenSportModeEnteredEventOccuredCurrentResponsivenessModeChangesToSport()
        {
            var dashboardState = new DashboardStateReadModel();
            var sportModeEntered = new SportModeEntered();

            dashboardState.Apply(sportModeEntered);

            Assert.AreEqual(dashboardState.CurrentResponsivenessMode, "SPORT");
        }

        [Test]
        public void WhenEconomicModeEnteredEventOccuredCurrentResponsivenessModeChangesToEconomic()
        {
            var dashboardState = new DashboardStateReadModel();
            var economicModeEntered = new EconomicModeEntered();

            dashboardState.Apply(economicModeEntered);

            Assert.AreEqual(dashboardState.CurrentResponsivenessMode, "ECONOMIC");
        }

        [Test]
        public void WhenAggressivenessLevelSelectedEventOccuredCurrentAggressivenessLevelChanges()
        {
            var dashboardState = new DashboardStateReadModel();
            var aggressivenessLevelSelected = new AggressivenessLevelSelected(AggressivenessLevel.Second);

            dashboardState.Apply(aggressivenessLevelSelected);

            Assert.AreEqual(dashboardState.CurrentAggressivenessLevel, AggressivenessLevel.Second.ToString());
        }

        [Test]
        public void WhenRPMChangedEventOccuredCurrentRPMChanges()
        {
            var dashboardState = new DashboardStateReadModel();
            var rpmChanged = new RpmChanged(new Rpm(1500d));

            dashboardState.Apply(rpmChanged);

            Assert.AreEqual(dashboardState.CurrentRpm, 1500d.ToString());
        }

        [Test]
        public void WhenManualGearshiftingModeEnteredEventOccuredManualModeIsEnabled()
        {
            var dashboardState = new DashboardStateReadModel();
            var manualGearshiftingModeEntered = new ManualGearshiftingModeEntered();

            dashboardState.Apply(manualGearshiftingModeEntered);

            Assert.IsTrue(dashboardState.IsInManualGearshiftingMode);
        }

        [Test]
        public void WhenManualGearshiftingModeEnteredEventOccuredManualModeIsDisabled()
        {
            var dashboardState = new DashboardStateReadModel();
            var manualGearshiftingModeExited = new ManualGearshiftingModeExited();

            dashboardState.Apply(manualGearshiftingModeExited);

            Assert.IsFalse(dashboardState.IsInManualGearshiftingMode);
        }
    }
}
