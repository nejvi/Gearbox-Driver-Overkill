using GearboxDriver.Seedwork;

namespace GearboxDriver.PublishedLanguage.Towing
{
    public class TrailerHookBecameOccupied : IEvent
    {
        public override string ToString()
        {
            return $"Trailer hook has become occupied.";
        }
    }
}
