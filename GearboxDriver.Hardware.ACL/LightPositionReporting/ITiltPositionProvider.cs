namespace GearboxDriver.Hardware.ACL.LightPositionReporting
{
    public enum TiltPosition
    {
        Upwards,
        Downwards,
        Balanced
    }

    public interface ITiltPositionProvider
    {
        TiltPosition GetCurrentLights();
    }
}
