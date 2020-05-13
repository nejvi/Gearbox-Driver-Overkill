using GearboxDriver.Gearshift;
using GearboxDriver.Seedwork;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace GearboxDriver.Processes.Test
{
    public class ProcessMaangerPoolTests
    {
        [Test]
        public void CannotAddSameProcessManagersTwice()
        {
            var processManagerPool = new ProcessPool();

            var processManager = new Mock<IProcessManager>();

            Assert.Throws<ArgumentException>(() =>
            {
                processManagerPool.Add(processManager.Object);
                processManagerPool.Add(processManager.Object);
            });
        }

        [Test]
        public void CannotRemoveSameProcessManagerTwice()
        {
            var processManagerPool = new ProcessPool();
            var serviceMock = new Mock<IGearshiftService>();
            var processManager = new SmoothBrakingWithTrailerAttached(serviceMock.Object);

            Assert.Throws<ArgumentException>(() =>
            {
                processManagerPool.Add(processManager);
                processManagerPool.Remove(typeof(SmoothBrakingWithTrailerAttached));
                processManagerPool.Remove(typeof(SmoothBrakingWithTrailerAttached));
            });
        }
    }
}
