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

        public override string ToString()
        {
            if (PreviousGear.Value == EnteredGear.Value)
                return $"Gear changed to 0.";

            return $"Gear changed from {PreviousGear.Value} to {EnteredGear.Value}";
        }
    }
}
