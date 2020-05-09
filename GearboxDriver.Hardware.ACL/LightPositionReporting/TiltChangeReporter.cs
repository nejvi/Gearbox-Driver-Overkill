namespace GearboxDriver.Hardware.ACL.LightPositionReporting
{
    public class TiltChangeReporter
    {
        private readonly IEventBus _eventBus;
        private readonly ITiltPositionProvider _tiltProvider;
        private LightsPosition LastReportedLights { get; set; }

        public TiltChangeReporter(IEventBus eventBus, ITiltPositionProvider tiltProvider)
        {
            _eventBus = eventBus;
            _tiltProvider = tiltProvider;
        }

        public void TryToReport()
        {
            var lights = _tiltProvider.GetCurrentLights();
            if (LastReportedLights == lights)
                return;

            _eventBus.SendEvent(new LightsPositionChanged(lights)); // VehicleTiltedUphill, VehicleTiltedDownhill, VehicleTiledToStraightPosition
            LastReportedLights = lights;
        }
    }
}
