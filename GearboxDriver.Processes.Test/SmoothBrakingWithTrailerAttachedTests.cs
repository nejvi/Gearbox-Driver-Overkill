using GearboxDriver.Gearshift;
using GearboxDriver.Hardware.ACL;
using GearboxDriver.Seedwork;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GearboxDriver.Processes.Test
{
    public class SmoothBrakingWithTrailerAttachedTests
    {
        [Test]
        public void WhenVehicleTiltedDownHillCarIsMovingDownhill()
        {
            var serviceMock = new Mock<IGearshiftService>();
            var manager = new SmoothBrakingWithTrailerAttached(serviceMock.Object);

            var @event = new VehicleTiltedDownhill();

            manager.ApplyEvent(@event);

            
        }

    }
}
