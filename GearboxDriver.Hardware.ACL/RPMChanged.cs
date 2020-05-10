using GearboxDriver.Seedwork;

namespace GearboxDriver.Hardware.ACL
{
    public class RPMChanged : IEvent
    {
        public Rpm Rpm { get; }

        public RPMChanged(Rpm rpm)
        {
            Rpm = rpm;
        }
    }
}
