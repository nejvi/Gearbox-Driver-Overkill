using GearboxDriver.Hardware.API;
using NUnit.Framework;

namespace GearboxDriver.Hardware.ACL.Tests
{
    public class LeverAdapterTests
    {
        [Test]
        public void WhenSetDriveModeGearboxStateChangedToDrive()
        {
            var gearbox = new Gearbox();
            var leverAdapter = new LeverAdapter(gearbox);

            leverAdapter.SetDriveMode();

            Assert.AreEqual(gearbox.getState(), 1);
        }

        [Test]
        public void WhenSetNeutralModeGearboxStateChangedToNeutral()
        {
            var gearbox = new Gearbox();
            var leverAdapter = new LeverAdapter(gearbox);

            leverAdapter.SetNeutralMode();

            Assert.AreEqual(gearbox.getState(), 4);
        }

        [Test]
        public void WhenSetParkModeGearboxStateChangedToPark()
        {
            var gearbox = new Gearbox();
            var leverAdapter = new LeverAdapter(gearbox);

            leverAdapter.SetParkMode();

            Assert.AreEqual(gearbox.getState(), 2);
        }

        [Test]
        public void WhenSetReverseModeGearboxStateChangedToReverse()
        {
            var gearbox = new Gearbox();
            var leverAdapter = new LeverAdapter(gearbox);

            leverAdapter.SetReverseMode();

            Assert.AreEqual(gearbox.getState(), 3);
        }
    }
}
