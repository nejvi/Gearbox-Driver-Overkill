using GearboxDriver.Seedwork;

namespace GearboxDriver.PublishedLanguage.Transmission
{
    public class NeutralModeEntered : IEvent
    {
        public override string ToString()
        {
            return $"Neutral mode has been entered.";
        }
    }
}
