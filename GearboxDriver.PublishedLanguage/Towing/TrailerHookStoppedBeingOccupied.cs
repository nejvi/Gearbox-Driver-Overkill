using GearboxDriver.Seedwork;

namespace GearboxDriver.PublishedLanguage.Towing
{
    public class TrailerHookStoppedBeingOccupied : IEvent
    {
        public override string ToString()
        {
            return $"Trailer has stopped being occupied.";
        }
    }
}
