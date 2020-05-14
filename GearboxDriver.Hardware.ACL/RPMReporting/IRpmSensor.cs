using GearboxDriver.PublishedLanguage.Gearbox;

namespace GearboxDriver.Hardware.ACL.RpmReporting
{
    public interface IRpmSensor
    {
        Rpm GetCurrentRpm();
    }
}
