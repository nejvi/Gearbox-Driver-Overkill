namespace GearboxDriver.Gearshift.Negotiaton
{
    public class TargetGearDemand 
    {
        public GearNumber Gear { get; }

        public TargetGearDemand(GearNumber gear)
        {
            Gear = gear;
        }
    }
}