using GearboxDriver.Hardware.ACL.RPMReporting;
using GearboxDriver.Hardware.ACL.TiltPositionReporting;
using GearboxDriver.Hardware.ACL.VehicleSlippingReporting;
using GearboxDriver.Hardware.API;
using System;
using System.Collections.Generic;
using System.Text;

namespace GearboxDriver.Hardware.ACL
{
    public class ExternalSystemsAdapter : IRPMProvider, ITiltPositionProvider, ISlippingProvider
    {
        private ExternalSystems _externalSystems;

        public RPM GetCurrentRpm()
        {
            var currentRpm = _externalSystems.getCurrentRpm();

            return new RPM(currentRpm);
        }

        public TiltPosition GetTiltPosition()
        {
            var lightsPosition = _externalSystems.getLights().getLightsPosition();

            if (lightsPosition == null)
                throw new Exception("No option in vehicle.");

            if (lightsPosition <= 1 && lightsPosition >= 3)
                return TiltPosition.Downwards;
            else if (lightsPosition <= 7 && lightsPosition >= 10)
                return TiltPosition.Upwards;
            else
                return TiltPosition.Balanced;
        }

        public bool IsCurrentlySlipping()
        {
            var currentAngularSpeed = _externalSystems.getAngularSpeed();

            if (currentAngularSpeed >= 0.5d)
                return true;
            else
                return false;
        }
    }
}
