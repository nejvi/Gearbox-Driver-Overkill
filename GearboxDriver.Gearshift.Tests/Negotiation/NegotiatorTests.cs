using GearboxDriver.Gearshift.Negotiaton;
using GearboxDriver.Gearshift.Shifting;
using GearboxDriver.Hardware.ACL;
using NUnit.Framework;

namespace GearboxDriver.Gearshift.Tests.Negotiation
{
    public class NegotiatorTests
    {
        [Test]
        public void YieldsByDefault()
        {
            var negotiator = new Negotiator();

            var program = negotiator.Negotiate();

            Assert.True(program is YieldingShiftingProgram);
        }

        [Test]
        public void GivenOnlyFollowRpmDemandItWinsTheNegotiation()
        {
            var negotiator = new Negotiator();
            negotiator.Issue(new FollowRpmDemand(new Rpm(4000), new Rpm(1000)));

            var program = negotiator.Negotiate();

            Assert.True(program is RpmBasedShiftingProgram);
        }

        [Test]
        public void GivenOnlyTargetGearDemandItWinsTheNegotiation()
        {
            var negotiator = new Negotiator();
            negotiator.Issue(new TargetGearDemand(new GearNumber(3)));

            var program = negotiator.Negotiate();

            Assert.True(program is GearTargetingShiftingProgram);
        }

        [Test]
        public void GivenOnlyYieldDemandItWinsTheNegotiation()
        {
            var negotiator = new Negotiator();
            negotiator.Issue(new YieldDemand());

            var program = negotiator.Negotiate();

            Assert.True(program is YieldingShiftingProgram);
        }
    }
}
