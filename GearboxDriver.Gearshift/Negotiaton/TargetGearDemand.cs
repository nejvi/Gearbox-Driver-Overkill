namespace GearboxDriver.Gearshift.Negotiaton
{
    public class TargetGearDemand 
    {
        public int Gear { get; } // todo Value Object

        public TargetGearDemand(int gear)
        {
            Gear = gear;
        }
    }
}