namespace GearboxDriver.Hardware.ACL.RPMReporting
{
    public class RPMReporter
    {
        private readonly IEventBus _eventBus;
        private readonly IRPMProvider _rpmProvider;
        private RPM _lastReportedRpm { get; set; }

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

            _eventBus.SendEvent(new RPMChanged(rpm));
            _lastReportedRpm = rpm;
        }
    }
}
