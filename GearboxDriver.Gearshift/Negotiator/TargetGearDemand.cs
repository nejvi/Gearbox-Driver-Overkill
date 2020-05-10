using System;

namespace GearboxDriver.Gearshift.Negotiator
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