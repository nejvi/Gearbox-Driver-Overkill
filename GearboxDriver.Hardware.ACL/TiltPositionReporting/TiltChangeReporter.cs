namespace GearboxDriver.Hardware.ACL.TiltPositionReporting
{
    public class TiltChangeReporter
    {
        private readonly IEventBus _eventBus;
        private readonly ITiltPositionProvider _tiltProvider;
        private TiltPosition LastReportedVehicleTiltPosition { get; set; }
        private bool isEverReported { get; set; }

        public TiltChangeReporter(IEventBus eventBus, ITiltPositionProvider tiltProvider)
        {
            _eventBus = eventBus;
            _tiltProvider = tiltProvider;
        }

        public void TryToReport()
        {
            var vehicleTiltPosition = _tiltProvider.GetTiltPosition();
            if (LastReportedVehicleTiltPosition == vehicleTiltPosition && isEverReported)
                return;

            if (vehicleTiltPosition == TiltPosition.Balanced)
                _eventBus.SendEvent(new VehicleTiledToStraightPosition());

            if (vehicleTiltPosition == TiltPosition.Downwards)
                _eventBus.SendEvent(new VehicleTiltedDownhill());

            if (vehicleTiltPosition == TiltPosition.Upwards)
                _eventBus.SendEvent(new VehicleTiltedUphill());

            LastReportedVehicleTiltPosition = vehicleTiltPosition;
            isEverReported = true;
        }
    }
}
