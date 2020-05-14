using GearboxDriver.Seedwork;

namespace GearboxDriver.PublishedLanguage.Transmission
{
    public class ReverseModeEntered : IEvent
    {
        public override string ToString()
        {
            return $"Reverse mode has been entered.";
        }
    }
}
