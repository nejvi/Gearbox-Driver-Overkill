using GearboxDriver.Seedwork;

namespace GearboxDriver.Hardware.ACL
{
    public class RpmChanged : IEvent
    {
        public Rpm Rpm { get; }

        public RpmChanged(Rpm rpm)
        {
            Rpm = rpm;
        }
    }
}
