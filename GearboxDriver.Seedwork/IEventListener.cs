namespace GearboxDriver.Seedwork
{
    public interface IEventListener
    {
        void SendEvent(IEvent @event);
    }
}
