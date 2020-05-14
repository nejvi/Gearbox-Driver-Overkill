using GearboxDriver.Hardware.ACL.Runtime;
using GearboxDriver.PublishedLanguage.Gearbox;
using GearboxDriver.Seedwork;

namespace GearboxDriver.Hardware.ACL.RpmReporting
{
    public class RpmReporter : IReporter
    {
        private readonly IEventBus _eventBus;
        private readonly IRpmSensor _rpmSensor;
        private Rpm LastReportedRpm { get; set; }

        public RpmReporter(IEventBus eventBus, IRpmSensor rpmSensor)
        {
            _eventBus = eventBus;
            _rpmSensor = rpmSensor;
        }

        public void TryToReport()
        {
            var rpm = _rpmSensor.GetCurrentRpm();
            if (LastReportedRpm == rpm) 
                return;

            _eventBus.SendEvent(new RpmChanged(rpm));
            LastReportedRpm = rpm;
        }
    }
}
