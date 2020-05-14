using GearboxDriver.Seedwork;

namespace GearboxDriver.PublishedLanguage.Gearbox
{
    public class RpmChanged : IEvent
    {
        public Rpm NewRpm { get; }

        public RpmChanged(Rpm newRpm)
        {
            NewRpm = newRpm;
        }

        public override string ToString()
        {
            return $"Rpm changed to {NewRpm.Value}";
        }
    }
}
