using GearboxDriver.Cabin.ManualGearshifting;
using GearboxDriver.Cabin.ReadModel;
using GearboxDriver.Cabin.Responsiveness;
using GearboxDriver.Cabin.Transmission;
using GearboxDriver.Hardware.ACL;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GearboxDriver.Dashboard.Tests.ReadModels
{
    public class DashboardStateTests
    {
        [Test]
        public void WhenParkModeEnteredEventOccuredCurrentTransmissionModeChangesToPark()
        {
            var dashboardState = new DashboardState();
            var parkModeEntered = new ParkModeEntered();

            dashboardState.Apply(parkModeEntered);

            Assert.AreEqual(dashboardState.CurrentTransmissionMode, "PARK");
        }

        [Test]
        public void WhenReverseModeEnteredEventOccuredCurrentTransmissionModeChangesToReverse()
        {
            var dashboardState = new DashboardState();
            var reverseModeEntered = new ReverseModeEntered();

            dashboardState.Apply(reverseModeEntered);

            Assert.AreEqual(dashboardState.CurrentTransmissionMode, "REVERSE");
        }

        [Test]
        public void WhenNeutralModeEnteredEventOccuredCurrentTransmissionModeChangesToNeutral()
        {
            var dashboardState = new DashboardState();
            var neutralModeEntered = new NeutralModeEntered();

            dashboardState.Apply(neutralModeEntered);

            Assert.AreEqual(dashboardState.CurrentTransmissionMode, "NEUTRAL");
        }

        [Test]
        public void WhenDriveModeEnteredEventOccuredCurrentTransmissionModeChangesToDrive()
        {
            var dashboardState = new DashboardState();
            var driveModeEntered = new DriveModeEntered();

            dashboardState.Apply(driveModeEntered);

            Assert.AreEqual(dashboardState.CurrentTransmissionMode, "DRIVE");
        }

        [Test]
        public void WhenComfortModeEnteredEventOccuredCurrentResponsivenessModeChangesToComfort()
        {
            var dashboardState = new DashboardState();
            var comfortModeEntered = new ComfortModeEntered();

            dashboardState.Apply(comfortModeEntered);

            Assert.AreEqual(dashboardState.CurrentResponsivenessMode, "COMFORT");
        }

        [Test]
        public void WhenSportModeEnteredEventOccuredCurrentResponsivenessModeChangesToSport()
        {
            var dashboardState = new DashboardState();
            var sportModeEntered = new SportModeEntered();

            dashboardState.Apply(sportModeEntered);

            Assert.AreEqual(dashboardState.CurrentResponsivenessMode, "SPORT");
        }

        [Test]
        public void WhenEconomicModeEnteredEventOccuredCurrentResponsivenessModeChangesToEconomic()
        {
            var dashboardState = new DashboardState();
            var economicModeEntered = new EconomicModeEntered();

            dashboardState.Apply(economicModeEntered);

            Assert.AreEqual(dashboardState.CurrentResponsivenessMode, "ECONOMIC");
        }

        [Test]
        public void WhenAggressivenessLevelSelectedEventOccuredCurrentAggressivenessLevelChanges()
        {
            var dashboardState = new DashboardState();
            var aggressivenessLevelSelected = new AggressivenessLevelSelected(AggressivenessLevel.Second);

            dashboardState.Apply(aggressivenessLevelSelected);

            Assert.AreEqual(dashboardState.CurrentAggressivenessLevel, AggressivenessLevel.Second.ToString());
        }

        [Test]
        public void WhenRPMChangedEventOccuredCurrentRPMChanges()
        {
            var dashboardState = new DashboardState();
            var rpmChanged = new RpmChanged(new Rpm(1500d));

            dashboardState.Apply(rpmChanged);

            Assert.AreEqual(dashboardState.CurrentRpm, 1500d.ToString());
        }

        [Test]
        public void WhenManualGearshiftingModeEnteredEventOccuredManualModeIsEnabled()
        {
            var dashboardState = new DashboardState();
            var manualGearshiftingModeEntered = new ManualGearshiftingModeEntered();

            dashboardState.Apply(manualGearshiftingModeEntered);

            Assert.IsTrue(dashboardState.IsInManualGearshiftingMode);
        }

        [Test]
        public void WhenManualGearshiftingModeEnteredEventOccuredManualModeIsDisabled()
        {
            var dashboardState = new DashboardState();
            var manualGearshiftingModeExited = new ManualGearshiftingModeExited();

            dashboardState.Apply(manualGearshiftingModeExited);

            Assert.IsFalse(dashboardState.IsInManualGearshiftingMode);
        }
    }
}
