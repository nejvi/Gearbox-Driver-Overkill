using GearboxDriver.Seedwork;

namespace GearboxDriver.PublishedLanguage.Transmission
{
    public class ParkModeEntered : IEvent
    {
        public override string ToString()
        {
            return $"Park mode has been entered.";
        }
    }
}
