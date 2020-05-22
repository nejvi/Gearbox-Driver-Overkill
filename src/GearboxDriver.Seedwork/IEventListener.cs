namespace GearboxDriver.Seedwork
{
    public interface IEventListener
    {
        void HandleEvent(IEvent @event);
    }
}
