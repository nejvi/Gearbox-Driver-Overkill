using System;
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
            if (OldRpm.Value == NewRpm.Value)
                return "Rpm changed to 0.";

            return $"Rpm changed from {Math.Floor(OldRpm.Value)} to {Math.Floor(NewRpm.Value)}";
        }
    }
}
