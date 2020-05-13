using GearboxDriver.Hardware.ACL.Runtime;
using GearboxDriver.Seedwork;

namespace GearboxDriver.Hardware.ACL.RPMReporting
{
    public class RPMReporter : IReporter
    {
        private readonly IEventBus _eventBus;
        private readonly IRPMProvider _rpmProvider;
        private Rpm _lastReportedRpm { get; set; }

        public RPMReporter(IEventBus eventBus, IRPMProvider rpmProvider)
        {
            _eventBus = eventBus;
            _rpmProvider = rpmProvider;
        }

        public void TryToReport()
        {
            var rpm = _rpmProvider.GetCurrentRpm();
            if (_lastReportedRpm == rpm) 
                return;

            _eventBus.SendEvent(new RpmChanged(rpm));
            _lastReportedRpm = rpm;
        }
    }
}
