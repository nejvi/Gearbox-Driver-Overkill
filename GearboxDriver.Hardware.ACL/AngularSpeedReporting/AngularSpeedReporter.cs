using System;
using System.Collections.Generic;
using System.Text;

namespace GearboxDriver.Hardware.ACL.AngularSpeedReporting
{
    public class AngularSpeedReporter
    {
        private readonly IEventBus _eventBus;
        private readonly IAngularSpeedProvider _angularProvider;

        public AngularSpeed _lastReportedAngularSpeed { get; set; }

        public AngularSpeedReporter(IEventBus eventBus, IAngularSpeedProvider angularProvider)
        {
            _eventBus = eventBus;
            _angularProvider = angularProvider;
        }

        public void TryToReport()
        {
            var angularSpeed = _angularProvider.GetCurrentAngularSpeed();
            if (_lastReportedAngularSpeed == angularSpeed)
                return;

            _eventBus.SendEvent(new AngularSpeedChanged(angularSpeed));
            _lastReportedAngularSpeed = angularSpeed;
        }
    }
}
