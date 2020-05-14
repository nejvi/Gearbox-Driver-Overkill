using GearboxDriver.Gearshift.Shifting;
using GearboxDriver.Hardware.ACL.RpmReporting;
using GearboxDriver.Hardware.ACL.Runtime;
using GearboxDriver.PublishedLanguage.Gearbox;
using GearboxDriver.Seedwork;

namespace GearboxDriver.Hardware.ACL.CurrentGearReporting
{
    public class GearReporter : IReporter
    {
        private readonly IEventBus _eventBus;
        private readonly IGearbox _gearbox;
        private Gear LastReportedGear { get; set; }
        private bool HasEverReported { get; set; }

        public GearReporter(IEventBus eventBus, IGearbox gearbox)
        {
            _eventBus = eventBus;
            _gearbox = gearbox;
            LastReportedGear = new Gear(0);
        }

        public void TryToReport()
        {
            var gear = _gearbox.CurrentGear;
            if (LastReportedGear == gear && HasEverReported) 
                return;

            _eventBus.SendEvent(new GearChanged(gear, LastReportedGear));
            LastReportedGear = gear;
            HasEverReported = true;
        }
    }
}
