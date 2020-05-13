using GearboxDriver.Gearshift.Negotiation;
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

        [Test]
        public void RevokingTargetGearDemandMakesItGoBackToFollowRpmDemand()
        {
            var negotiator = new Negotiator();
            negotiator.Issue(_dummyTargetGearDemand);
            negotiator.Issue(_dummyFollowRpmDemand);
            negotiator.RevokeTargetGearDemand();

            var program = negotiator.Negotiate();

            Assert.True(program is RpmBasedShiftingProgram);
        }

        [Test]
        public void EngineBreakingDemandAltersAffectsTheProgramWhenInRpm()
        {
            var negotiator = new Negotiator();
            negotiator.Issue(_dummyFollowRpmDemand);
            negotiator.Issue(new EngineBrakingDemand());

            var program = negotiator.Negotiate();

            Assert.True(program is EngineBrakingShiftingProgram);
        }

        [Test]
        public void RevokingEngineBrakingProgramMakesNegotiatorReturnRpmBackAgain()
        {
            var negotiator = new Negotiator();
            negotiator.Issue(_dummyFollowRpmDemand);
            negotiator.Issue(new EngineBrakingDemand());
            negotiator.RevokeEngineBrakingDemand();

            var program = negotiator.Negotiate();

            Assert.True(program is RpmBasedShiftingProgram);
        }

        [Test]
        public void NegotiatedTargetGearProgramReflectsTheDemand()
        {
            var negotiator = new Negotiator();
            negotiator.Issue(new TargetGearDemand(new Gear(2)));

            var program = negotiator.Negotiate();

            Assert.AreEqual(SuggestedAction.Upshift, program.GetSuggestedActionFor(new Gear(1), new Rpm(1000)));
            Assert.AreEqual(SuggestedAction.Retain, program.GetSuggestedActionFor(new Gear(2), new Rpm(1000)));
            Assert.AreEqual(SuggestedAction.Downshift, program.GetSuggestedActionFor(new Gear(3), new Rpm(1000)));
        }

        [Test]
        public void NegotiatedFollowRpmProgramReflectsTheDemand()
        {
            var negotiator = new Negotiator();
            negotiator.Issue(_dummyFollowRpmDemand);

            var program = negotiator.Negotiate();

            var rpmInTheMiddleOfDemandedRange = new Rpm((_dummyFollowRpmDemand.ShiftpointRange.LowerShiftPoint.Value +
                                                         _dummyFollowRpmDemand.ShiftpointRange.UpperShiftPoint.Value) /
                                                        2);

            var rpmBelowDemandedRange = new Rpm(_dummyFollowRpmDemand.ShiftpointRange.LowerShiftPoint.Value - 1);

            var rpmAboveDemandedRange = new Rpm(_dummyFollowRpmDemand.ShiftpointRange.UpperShiftPoint.Value + 1);

            var anyGear = new Gear(2);

            Assert.AreEqual(SuggestedAction.Retain, program.GetSuggestedActionFor(anyGear, rpmInTheMiddleOfDemandedRange));
            Assert.AreEqual(SuggestedAction.Downshift, program.GetSuggestedActionFor(anyGear, rpmBelowDemandedRange));
            Assert.AreEqual(SuggestedAction.Upshift, program.GetSuggestedActionFor(anyGear, rpmAboveDemandedRange));
        }
    }
}
