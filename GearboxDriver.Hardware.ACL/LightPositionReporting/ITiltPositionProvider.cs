namespace GearboxDriver.Hardware.ACL.LightPositionReporting
{
    public interface ITiltPositionProvider
    {
        VehicleTiltPosition GetCurrentTiltPosition();
    }
}
