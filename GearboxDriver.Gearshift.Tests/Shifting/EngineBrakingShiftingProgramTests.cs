using GearboxDriver.Gearshift.Shifting;
using GearboxDriver.Hardware.ACL;
using GearboxDriver.PublishedLanguage.Gearbox;
using Moq;
using NUnit.Framework;

namespace GearboxDriver.Gearshift.Tests.Shifting
{
    class EngineBrakingShiftingProgramTests
    {
        [Test]
        public void DownshiftsWhenActionSuggestedByInnerProgramIsRetain()
        {
            var innerProgramMock = new Mock<IShiftingProgram>();
            innerProgramMock
                .Setup(x => x.GetSuggestedActionFor(It.IsAny<Gear>(), It.IsAny<Rpm>()))
                .Returns(SuggestedAction.Retain);

            var program = new EngineBrakingShiftingProgram(innerProgramMock.Object);

            Assert.AreEqual(SuggestedAction.Downshift, program.GetSuggestedActionFor(new Gear(2), new Rpm(2000)));
        }

        [Test]
        public void RetainsWhenActionSuggestedByInnerProgramIsDownshift()
        {
            var innerProgramMock = new Mock<IShiftingProgram>();
            innerProgramMock
                .Setup(x => x.GetSuggestedActionFor(It.IsAny<Gear>(), It.IsAny<Rpm>()))
                .Returns(SuggestedAction.Downshift);

            var program = new EngineBrakingShiftingProgram(innerProgramMock.Object);

            Assert.AreEqual(SuggestedAction.Retain, program.GetSuggestedActionFor(new Gear(2), new Rpm(2000)));
        }

        [Test]
        public void RetainsWhenActionSuggestedByInnerProgramIsUpshift()
        {
            var innerProgramMock = new Mock<IShiftingProgram>();
            innerProgramMock
                .Setup(x => x.GetSuggestedActionFor(It.IsAny<Gear>(), It.IsAny<Rpm>()))
                .Returns(SuggestedAction.Upshift);

            var program = new EngineBrakingShiftingProgram(innerProgramMock.Object);

            Assert.AreEqual(SuggestedAction.Retain, program.GetSuggestedActionFor(new Gear(2), new Rpm(2000)));
        }
    }
}
