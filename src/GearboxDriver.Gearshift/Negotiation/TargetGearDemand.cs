using GearboxDriver.PublishedLanguage.Gearbox;

namespace GearboxDriver.Gearshift.Negotiation
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