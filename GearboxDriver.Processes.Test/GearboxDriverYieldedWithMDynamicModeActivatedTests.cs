using GearboxDriver.Gearshift;
using GearboxDriver.Gearshift.Negotiaton;
using GearboxDriver.Hardware.ACL;
using Moq;
using Newtonsoft.Json.Schema;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace GearboxDriver.Processes.Test
{
    public class GearboxDriverYieldedWithMDynamicModeActivatedTests
    {
        [Test]
        public void WhenVehicleIsSlippingAbstainFromChangingGears()
        {
            var gearShifter = new AutomaticGearshifter();
            var negotiator = new Negotiator();
            var serviceMock = new Mock<GearshiftService>(negotiator, gearShifter);

            var processManager = new GearboxDriverYielededWithMDynamicModeActivated(serviceMock.Object);
            var @event = new VehicleStartedSlipping();

            processManager.ApplyEvent(@event);

            serviceMock.Verify(x => x.AbstainFromChangingGears(), Times.Once);
        }

        [Test]
        public void WhenVehicleStoppedSlippingStopAbstainingFromChangingGears()
        {
            var gearShifter = new AutomaticGearshifter();
            var negotiator = new Negotiator();
            var serviceMock = new Mock<GearshiftService>(negotiator, gearShifter);

            var processManager = new GearboxDriverYielededWithMDynamicModeActivated(serviceMock.Object);
            var @event = new VehicleStoppedSlipping();

            processManager.ApplyEvent(@event);

            serviceMock.Verify(x => x.StopAbstainingFromChangingGears(), Times.Once);
        }
    }
}
