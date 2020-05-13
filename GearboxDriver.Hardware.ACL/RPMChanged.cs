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

        public override string ToString()
        {
            return $"Rpm changed to {Rpm.Value}";
        }
    }
}
