using GearboxDriver.Cabin.ManualGearshifting;
using GearboxDriver.Cabin.MDynamic;
using GearboxDriver.Cabin.Towing;
using GearboxDriver.Hardware.ACL;
using GearboxDriver.Seedwork;

namespace GearboxDriver.Processes
{
    public class Dispatcher
    {
        private readonly ProcessManagerPool _pool;

        public Dispatcher()
        {
            _pool = new ProcessManagerPool();
        }

        public void ApplyEvent(IEvent @event)
        {
            ManageProcessesLifetime(@event);

            _pool.Dispatch(@event);
        }

        private void ManageProcessesLifetime(IEvent @event)
        {
            switch (@event)
            {
                case TrailerHookBecameOccupied _:
                    _pool.Add(new SmoothBrakingWithTrailerAttached());
                    break;
                case TrailerHookStoppedBeingOccupied _:
                    _pool.Remove(typeof(SmoothBrakingWithTrailerAttached));
                    break;
                case ManualGearshiftingModeEntered _:
                    _pool.Add(new NoInterferenceToGearshiftWithManualMode());
                    break;
                case ManualGearshiftingModeExited _:
                    _pool.Remove(typeof(NoInterferenceToGearshiftWithManualMode));
                    break;
                case MDynamicModeEntered _:
                    _pool.Add(new GearboxDriverYielededWithMDynamicModeActivated());
                    break;
                case MDynamicModeExited _:
                    _pool.Remove(typeof(GearboxDriverYielededWithMDynamicModeActivated));
                    break;
            }
        }

       
    }
}
