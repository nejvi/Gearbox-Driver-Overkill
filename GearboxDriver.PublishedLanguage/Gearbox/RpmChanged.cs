using GearboxDriver.Seedwork;

namespace GearboxDriver.PublishedLanguage.Gearbox
{
    public class RpmChanged : IEvent
    {
        public Rpm NewRpm { get; }
        public Rpm OldRpm { get; }

        public RpmChanged(Rpm oldRpm, Rpm newRpm)
        {
            OldRpm = oldRpm;
            NewRpm = newRpm;
        }

        public override string ToString()
        {
            return $"Rpm changed from {OldRpm.Value} to {NewRpm.Value}";
        }
    }
}
