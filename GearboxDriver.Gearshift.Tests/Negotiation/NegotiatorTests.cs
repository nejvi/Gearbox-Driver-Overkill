using GearboxDriver.Gearshift.Negotiaton;
using GearboxDriver.Gearshift.Shifting;
using GearboxDriver.Hardware.ACL;
using NUnit.Framework;

namespace GearboxDriver.Gearshift.Tests.Negotiation
{
    public class NegotiatorTests
    {
        private readonly FollowRpmDemand _dummyFollowRpmDemand = new FollowRpmDemand(new ShiftpointRange(new Rpm(1000), new Rpm(4000)));
        private readonly TargetGearDemand _dummyTargetGearDemand = new TargetGearDemand(new Gear(3));
        private readonly YieldDemand _dummyYieldDemand = new YieldDemand();

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
            negotiator.Issue(_dummyFollowRpmDemand);

            var program = negotiator.Negotiate();

            Assert.True(program is RpmBasedShiftingProgram);
        }

        [Test]
        public void GivenOnlyTargetGearDemandItWinsTheNegotiation()
        {
            var negotiator = new Negotiator();
            negotiator.Issue(_dummyTargetGearDemand);

            var program = negotiator.Negotiate();

            Assert.True(program is GearTargetingShiftingProgram);
        }

        [Test]
        public void GivenOnlyYieldDemandItWinsTheNegotiation()
        {
            var negotiator = new Negotiator();
            negotiator.Issue(_dummyYieldDemand);

            var program = negotiator.Negotiate();

            Assert.True(program is YieldingShiftingProgram);
        }

        [Test]
        public void YieldDemandWinsOverAllOtherDemands()
        {
            var negotiator = new Negotiator();
            negotiator.Issue(_dummyYieldDemand);
            negotiator.Issue(_dummyTargetGearDemand);
            negotiator.Issue(_dummyFollowRpmDemand);

            var program = negotiator.Negotiate();

            Assert.True(program is YieldingShiftingProgram);
        }

        [Test]
        public void TargetGearDemandWinsOverFollowRpmDemand()
        {
            var negotiator = new Negotiator();
            negotiator.Issue(_dummyTargetGearDemand);
            negotiator.Issue(_dummyFollowRpmDemand);

            var program = negotiator.Negotiate();

            Assert.True(program is GearTargetingShiftingProgram);
        }
    }
}
