using GearboxDriver.Gearshift;
using GearboxDriver.Gearshift.Negotiaton;
using GearboxDriver.Hardware.ACL;
using GearboxDriver.Seedwork;
using Moq;
using Newtonsoft.Json.Schema;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using GearboxDriver.Gearshift.Shifting;

namespace GearboxDriver.Processes.Test
{
    public class GearboxDriverYieldedWithMDynamicModeActivatedTests
    {
        [Test]
        public void WhenVehicleIsSlippingAbstainFromChangingGears()
        {
            var serviceMock = new Mock<IGearshiftService>();

            var processManager = new GearboxDriverYielededWithMDynamicModeActivated(serviceMock.Object);
            var @event = new VehicleStartedSlipping();

            processManager.ApplyEvent(@event);

            serviceMock.Verify(x => x.AbstainFromChangingGears(), Times.Once);
        }

        [Test]
        public void WhenVehicleStoppedSlippingStopAbstainingFromChangingGears()
        {
            var serviceMock = new Mock<IGearshiftService>();

            var processManager = new GearboxDriverYielededWithMDynamicModeActivated(serviceMock.Object);
            var @event = new VehicleStoppedSlipping();

            processManager.ApplyEvent(@event);

            serviceMock.Verify(x => x.StopAbstainingFromChangingGears(), Times.Once);
        }
    }
}
