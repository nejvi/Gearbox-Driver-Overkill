namespace GearboxDriver.Gearshift.Negotiaton
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