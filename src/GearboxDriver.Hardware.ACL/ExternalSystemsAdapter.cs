using GearboxDriver.Hardware.ACL.TiltPositionReporting;
using GearboxDriver.Hardware.ACL.VehicleSlippingReporting;
using GearboxDriver.Hardware.API;
using System;
using GearboxDriver.Hardware.ACL.RpmReporting;
using GearboxDriver.PublishedLanguage.Gearbox;

namespace GearboxDriver.Hardware.ACL
{
    public class ExternalSystemsAdapter : IRpmSensor, ITiltPositionSensor, ISlippingSensor
    {
        private readonly ExternalSystems _externalSystems;

        public ExternalSystemsAdapter(ExternalSystems externalSystems)
        {
            _externalSystems = externalSystems;
        }

        public Rpm GetCurrentRpm()
        {
            var currentRpm = _externalSystems.getCurrentRpm();

            return new Rpm(currentRpm);
        }

        public TiltPosition GetTiltPosition()
        {
            var lightsPosition = _externalSystems.getLights()?.getLightsPosition();

            if (lightsPosition == null)
                throw new Exception("No option in vehicle.");

            if (lightsPosition >= 1 && lightsPosition <= 3)
                return TiltPosition.Downwards;
            else if (lightsPosition >= 7 && lightsPosition <= 10)
                return TiltPosition.Upwards;
            else
                return TiltPosition.Balanced;
        }

        public bool IsCurrentlySlipping()
        {
            var currentAngularSpeed = _externalSystems.getAngularSpeed();

            if (currentAngularSpeed >= 250d)
                return true;
            else
                return false;
        }

        public bool SupportsTiltPosition() =>      
            !(_externalSystems.getLights()?.getLightsPosition() is null);
    }
}
