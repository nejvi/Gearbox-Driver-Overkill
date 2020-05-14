using GearboxDriver.Gearshift.Shifting;
using GearboxDriver.PublishedLanguage.Gearbox;
using Moq;
using NUnit.Framework;

namespace GearboxDriver.Gearshift.Tests.Shifting
{
    public class AutomaticGearshifterTests
    {
        private Mock<IGearbox> _gearboxMock;
        private Mock<IShiftingProgram> _programMock;
        private AutomaticGearshifter _gearshifter;

        [SetUp]
        public void SetUp()
        {
            _gearboxMock = new Mock<IGearbox>();
            _programMock = new Mock<IShiftingProgram>();
            _gearshifter = new AutomaticGearshifter(_gearboxMock.Object);
        }

        [Test]
        public void YieldsByDefault()
        {
            _gearshifter.HandleRpmChange(new Rpm(300));
            _gearshifter.HandleRpmChange(new Rpm(1000));
            _gearshifter.HandleRpmChange(new Rpm(2000));

            _gearboxMock.Verify(x => x.CurrentGear);
            _gearboxMock.VerifyNoOtherCalls();
        }

        [Test]
        public void DownshiftsWhenSuggestedActionIsDownshift()
        {
            _programMock.Setup(x => x.GetSuggestedActionFor(It.IsAny<Gear>(), It.IsAny<Rpm>())).Returns(SuggestedAction.Downshift);
            _gearshifter.SetProgram(_programMock.Object);
            
            _gearshifter.HandleRpmChange(new Rpm(2000));

            _gearboxMock.Verify(x => x.Downshift());
        }

        [Test]
        public void UpshiftsWhenSuggestedActionIsUpshift()
        {
            _programMock.Setup(x => x.GetSuggestedActionFor(It.IsAny<Gear>(), It.IsAny<Rpm>())).Returns(SuggestedAction.Upshift);
            _gearshifter.SetProgram(_programMock.Object);
           
            _gearshifter.HandleRpmChange(new Rpm(2000));

            _gearboxMock.Verify(x => x.Upshift());
        }


        [Test]
        public void RetainsWhenSuggestedActionIsRetain()
        {
            _programMock.Setup(x => x.GetSuggestedActionFor(It.IsAny<Gear>(), It.IsAny<Rpm>())).Returns(SuggestedAction.Retain);
            _gearshifter.SetProgram(_programMock.Object);

            _gearshifter.HandleRpmChange(new Rpm(2000));

            _gearboxMock.Verify(x => x.CurrentGear);
            _gearboxMock.VerifyNoOtherCalls();
        }

        [Test]
        public void QueriesTheProgramAboutReceivedFps()
        {
            var rpm = new Rpm(2000);

            _programMock.Setup(x => x.GetSuggestedActionFor(It.IsAny<Gear>(), rpm)).Returns(SuggestedAction.Retain);
            _gearshifter.SetProgram(_programMock.Object);

            _gearshifter.HandleRpmChange(new Rpm(2000));

            _programMock.Verify(x => x.GetSuggestedActionFor(It.IsAny<Gear>(), rpm));
        }

        [Test]
        public void QueriesTheProgramGivingTheGearFromGearbox()
        {
            var rpm = new Rpm(2000);

            _gearboxMock.Setup(x => x.CurrentGear).Returns(new Gear(3));

            _programMock.Setup(x => x.GetSuggestedActionFor(new Gear(3), It.IsAny<Rpm>())).Returns(SuggestedAction.Retain);
            _gearshifter.SetProgram(_programMock.Object);

            _gearshifter.HandleRpmChange(new Rpm(2000));

            _programMock.Verify(x => x.GetSuggestedActionFor(It.IsAny<Gear>(), rpm));
        }

        [Test]
        public void ChangesBehaviourAlongWithChangedProgram()
        {
            var rpm = new Rpm(2000);

            _programMock.Setup(x => x.GetSuggestedActionFor(It.IsAny<Gear>(), rpm)).Returns(SuggestedAction.Upshift);
            _gearshifter.SetProgram(_programMock.Object);
            _gearshifter.HandleRpmChange(new Rpm(2000));

            var secondProgramMock = new Mock<IShiftingProgram>();
            secondProgramMock.Setup(x => x.GetSuggestedActionFor(It.IsAny<Gear>(), rpm)).Returns(SuggestedAction.Downshift);
            _gearshifter.SetProgram(secondProgramMock.Object);
            _gearshifter.HandleRpmChange(new Rpm(2000));

            _gearboxMock.Verify(x => x.Upshift());
            _gearboxMock.Verify(x => x.Downshift());
        }
    }
}
