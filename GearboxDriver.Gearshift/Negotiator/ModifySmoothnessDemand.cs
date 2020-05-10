using System;

namespace GearboxDriver.Gearshift.Negotiator
{
    public class ModifySmoothnessDemand 
    {
        public double Percentage { get; } // todo Value Object

        public ModifySmoothnessDemand(double percentage)
        {
            Percentage = percentage;
        }
    }
}