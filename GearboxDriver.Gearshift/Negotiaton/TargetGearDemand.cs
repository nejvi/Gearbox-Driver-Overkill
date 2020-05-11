namespace GearboxDriver.Gearshift.Negotiaton
{
    public class TargetGearDemand 
    {
        public Gear Gear { get; }

        public TargetGearDemand(Gear gear)
        {
            Gear = gear;
        }
    }
}