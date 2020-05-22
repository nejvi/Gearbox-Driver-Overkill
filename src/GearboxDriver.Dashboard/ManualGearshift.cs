using System.Collections.Generic;
using GearboxDriver.PublishedLanguage.ManualGearshifting;
using GearboxDriver.Seedwork;

namespace GearboxDriver.CabinControls
{
    public class ManualGearshift
    {
        private bool InManualMode { get; set; }

        public IReadOnlyCollection<IEvent> Upshift()
        {
            return new List<IEvent>{ new GearUpshiftedManually() };
        }

        public IReadOnlyCollection<IEvent> Downshift()
        {
            return new List<IEvent> { new GearDownshiftedManually() };
        }

        public IReadOnlyCollection<IEvent> EnterManualMode()
        {
            if (InManualMode)
                throw new DomainRuleViolatedException("Manual mode is already entered.");

            InManualMode = true;
            return new List<IEvent> { new ManualGearshiftingModeEntered() };
        }

        public IReadOnlyCollection<IEvent> ExitManualMode()
        {
            if (!InManualMode)
                throw new DomainRuleViolatedException("Manual mode is not enabled.");

            InManualMode = false;
            return new List<IEvent> { new ManualGearshiftingModeExited() };
        }
    }
}
