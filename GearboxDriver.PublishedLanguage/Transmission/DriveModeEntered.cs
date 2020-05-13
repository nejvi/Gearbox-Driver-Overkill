using GearboxDriver.Seedwork;

namespace GearboxDriver.PublishedLanguage.Transmission
{
    public class DriveModeEntered : IEvent
    {
        public override string ToString()
        {
            return $"Drive mode has been entered.";
        }
    }
}
