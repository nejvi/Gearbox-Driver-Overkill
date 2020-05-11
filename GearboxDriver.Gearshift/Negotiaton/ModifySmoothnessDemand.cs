namespace GearboxDriver.Gearshift.Negotiaton
{
    public class ModifySmoothnessDemand 
    {
        public Percentage Percentage { get; }

        public ModifySmoothnessDemand(Percentage percentage)
        {
            Percentage = percentage;
        }
    }
}