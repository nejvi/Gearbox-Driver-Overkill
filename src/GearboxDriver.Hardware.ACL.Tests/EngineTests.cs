using GearboxDriver.Hardware.ACL.Runtime;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace GearboxDriver.Hardware.ACL.Tests
{
    public class EngineTests
    {
        [Test]
        public void CannotCreateEngineWithNoReporters()
        {
            Assert.Throws<EngineStartupException>(() =>
            {
                new Engine(new List<IReporter>());
            });
        }

        [Test]
        public void CannotStartEngineTwice()
        {
            var reporters = new List<IReporter>();
            reporters.Add(new Mock<IReporter>().Object);

            var engine = new Engine(reporters);

            Assert.Throws<EngineStartupException>(() =>
            {
                engine.Start();
                engine.Start();
            });
        }

        [Test]
        public void EngineCallsEveryReporter()
        {
            var reporters = new List<IReporter>();
            var reporterMocks = new List<Mock<IReporter>>();

            reporterMocks.Add(new Mock<IReporter>());
            reporterMocks.Add(new Mock<IReporter>());
            reporterMocks.Add(new Mock<IReporter>());

            var engine = new Engine(reporterMocks.Select(x => x.Object).ToList());

            engine.Start();

            foreach(var reporterMock in reporterMocks)
                reporterMock.Verify(x => x.TryToReport());        
        }
    }
}
