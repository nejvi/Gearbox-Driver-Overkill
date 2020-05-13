using GearboxDriver.PublishedLanguage.Transmission;
using Moq;
using NUnit.Framework;

namespace GearboxDriver.Hardware.ACL.Tests
{
    public class TransmissionEventListenerTests
    {
        [Test]
        public void WhenParkModeEnteredEventOccuredGearboxChangeToParkState()
        {
            var leverMock = new Mock<ILever>();
            var listener = new TransmissionEventListener(leverMock.Object);
            var @event = new ParkModeEntered();

            listener.HandleEvent(@event);

            leverMock.Verify(x => x.SetParkMode(), Times.Once);
        }

        [Test]
        public void WhenNeutralModeEnteredEventOccuredGearboxChangeToNeutralState()
        {
            var leverMock = new Mock<ILever>();
            var listener = new TransmissionEventListener(leverMock.Object);
            var @event = new NeutralModeEntered();

            listener.HandleEvent(@event);

            leverMock.Verify(x => x.SetNeutralMode(), Times.Once);
        }

        [Test]
        public void WhenDriveModeEnteredEventOccuredGearboxChangeToDriveState()
        {
            var leverMock = new Mock<ILever>();
            var listener = new TransmissionEventListener(leverMock.Object);
            var @event = new DriveModeEntered();

            listener.HandleEvent(@event);

            leverMock.Verify(x => x.SetDriveMode(), Times.Once);
        }

        [Test]
        public void WhenReverseModeEnteredEventOccuredGearboxChangeToReverseState()
        {
            var leverMock = new Mock<ILever>();
            var listener = new TransmissionEventListener(leverMock.Object);
            var @event = new ReverseModeEntered();

            listener.HandleEvent(@event);

            leverMock.Verify(x => x.SetReverseMode(), Times.Once);
        }


    }
}
