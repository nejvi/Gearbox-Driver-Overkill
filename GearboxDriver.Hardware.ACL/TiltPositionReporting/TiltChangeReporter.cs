using GearboxDriver.Hardware.ACL.Runtime;
using GearboxDriver.PublishedLanguage.VehicleMotion;
using GearboxDriver.Seedwork;

namespace GearboxDriver.Hardware.ACL.TiltPositionReporting
{
    public class TiltChangeReporter : IReporter
    {
        private readonly IEventBus _eventBus;
        private readonly ITiltPositionSensor _tiltSensor;
        private TiltPosition LastReportedVehicleTiltPosition { get; set; }
        private bool HasEverReported { get; set; }

        public TiltChangeReporter(IEventBus eventBus, ITiltPositionSensor tiltSensor)
        {
            _eventBus = eventBus;
            _tiltSensor = tiltSensor;
        }

        public void TryToReport()
        {
            var vehicleTiltPosition = _tiltSensor.GetTiltPosition();

            if (LastReportedVehicleTiltPosition == vehicleTiltPosition && HasEverReported)
                return;

            if (vehicleTiltPosition == TiltPosition.Balanced)
                _eventBus.SendEvent(new VehicleTiledToStraightPosition());

            if (vehicleTiltPosition == TiltPosition.Downwards)
                _eventBus.SendEvent(new VehicleTiltedDownhill());

            if (vehicleTiltPosition == TiltPosition.Upwards)
                _eventBus.SendEvent(new VehicleTiltedUphill());

            LastReportedVehicleTiltPosition = vehicleTiltPosition;
            HasEverReported = true;
        }
    }
}
