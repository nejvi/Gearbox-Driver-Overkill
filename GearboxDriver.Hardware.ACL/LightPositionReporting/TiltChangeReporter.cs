namespace GearboxDriver.Hardware.ACL.LightPositionReporting
{
    public class TiltChangeReporter
    {
        private readonly IEventBus _eventBus;
        private readonly ITiltPositionProvider _tiltProvider;
        private VehicleTiltPosition LastReportedVehicleTiltPosition { get; set; }

        public TiltChangeReporter(IEventBus eventBus, ITiltPositionProvider tiltProvider)
        {
            _eventBus = eventBus;
            _tiltProvider = tiltProvider;
        }

        public void TryToReport()
        {
            var vehicleTiltPosition = _tiltProvider.GetTiltPosition();
            if (LastReportedVehicleTiltPosition == vehicleTiltPosition)
                return;

            if (vehicleTiltPosition.Value == TiltPosition.Balanced)
                _eventBus.SendEvent(new VehicleTiledToStraightPosition(vehicleTiltPosition));

            if(vehicleTiltPosition.Value == TiltPosition.Downwards)

            if(vehicleTiltPosition.Value == TiltPosition.Upwards)

            LastReportedVehicleTiltPosition = vehicleTiltPosition;
        }
    }
}
