using System;
using System.Collections.Generic;
using System.Text;

namespace GearboxDriver.Hardware.ACL.LightsReporting
{
    public class LightsPositionReporter
    {
        private readonly IEventBus _eventBus;
        private readonly ILightsPositionProvider _lightsProvider;
        private LightsPosition _lastReportedLights { get; set; }

        public LightsPositionReporter(IEventBus eventBus, ILightsPositionProvider lightsProvider)
        {
            _eventBus = eventBus;
            _lightsProvider = lightsProvider;
        }

        public void TryToReport()
        {
            var lights = _lightsProvider.GetCurrentLights();
            if (_lastReportedLights == lights)
                return;

            _eventBus.SendEvent(new LightsPositionChanged(lights));
            _lastReportedLights = lights;
        }
    }
}
