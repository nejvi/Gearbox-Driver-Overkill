using GearboxDriver.Seedwork;

namespace GearboxDriver.PublishedLanguage.Gearbox
{
    public class GearChanged : IEvent
    {
        public Gear EnteredGear { get; }
        public Gear PreviousGear { get; }

        public GearChanged(Gear enteredGear, Gear previousGear)
        {
            EnteredGear = enteredGear;
            PreviousGear = previousGear;
        }
    }
}
