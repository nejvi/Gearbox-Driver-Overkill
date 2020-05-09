namespace GearboxDriver.Hardware.ACL
{
    public class RPMChanged : IEvent
    {
        public RPM Rpm { get; }

        public RPMChanged(RPM rpm)
        {
            Rpm = rpm;
        }
    }
}
