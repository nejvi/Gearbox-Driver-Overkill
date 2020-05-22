using GearboxDriver.Gearshift;
using Moq;
using NUnit.Framework;
using System;

namespace GearboxDriver.Processes.Test
{
    public class ProcessPoolTests
    {
        [Test]
        public void CannotAddSameProcessTwice()
        {
            var processManagerPool = new ProcessPool();

            var processManager = new Mock<IProcess>();

            Assert.Throws<ArgumentException>(() =>
            {
                processManagerPool.Add(processManager.Object);
                processManagerPool.Add(processManager.Object);
            });
        }

        [Test]
        public void CannotRemoveSameProcessTwice()
        {
            var processManagerPool = new ProcessPool();
            var serviceMock = new Mock<IGearshiftService>();
            var processManager = new TrailerEngineBrakingDetectionProcess(serviceMock.Object);

            Assert.Throws<ArgumentException>(() =>
            {
                processManagerPool.Add(processManager);
                processManagerPool.Remove(typeof(TrailerEngineBrakingDetectionProcess));
                processManagerPool.Remove(typeof(TrailerEngineBrakingDetectionProcess));
            });
        }
    }
}
