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
            var processManagerPool = new ProcessManagerPool();

            var processManager = new Mock<IProcessManager>();

            Assert.Throws<ArgumentNullException>(() =>
            {
                processManagerPool.Add(processManager.Object);
                processManagerPool.Add(processManager.Object);
            });
        }

        [Test]
        public void CannotRemoveSameProcessManagerTwice()
        {
            var processManagerPool = new ProcessManagerPool();

            var processManager = new SmoothBrakingWithTrailerAttached();

            Assert.Throws<ArgumentNullException>(() =>
            {
                processManagerPool.Add(processManager);
                processManagerPool.Remove(typeof(SmoothBrakingWithTrailerAttached));
                processManagerPool.Remove(typeof(SmoothBrakingWithTrailerAttached));
            });
        }
    }
}
