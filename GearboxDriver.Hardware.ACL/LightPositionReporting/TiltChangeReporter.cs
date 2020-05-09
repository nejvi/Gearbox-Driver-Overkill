namespace GearboxDriver.Hardware.ACL.LightPositionReporting
{
    public class TiltChangeReporter
    {
        private readonly IEventBus _eventBus;
        private readonly ITiltPositionProvider _tiltProvider;
        private VehicleTiltPosition LastReportedTiltPosition { get; set; }

        public TiltChangeReporter(IEventBus eventBus, ITiltPositionProvider tiltProvider)
        {
            _eventBus = eventBus;
            _tiltProvider = tiltProvider;
        }

        public void TryToReport()
        {
            var tiltPosition = _tiltProvider.GetCurrentTiltPosition();
            if (LastReportedTiltPosition == tiltPosition)
                return;

            if (tiltPosition.Value == TiltPosition.Balanced)
                _eventBus.SendEvent(new VehicleTiledToStraightPosition(tiltPosition));

            if (tiltPosition.Value == TiltPosition.Downwards)
                _eventBus.SendEvent(new VehicleTiltedDownhill(tiltPosition));

            if (tiltPosition.Value == TiltPosition.Upwards)
                _eventBus.SendEvent(new VehicleTiltedUphill(tiltPosition));

            LastReportedTiltPosition = tiltPosition;
        }
    }
}
