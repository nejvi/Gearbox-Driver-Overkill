using GearboxDriver.Gearshift.Shifting;
using GearboxDriver.Seedwork;

namespace GearboxDriver.Gearshift
{
    public class GearshiftStartup
    {
        private readonly IEventBus _eventBus;
        private readonly AutomaticGearshifter _gearshifter;

        public GearshiftStartup(IEventBus eventBus, AutomaticGearshifter gearshifter)
        {
            _eventBus = eventBus;
            _gearshifter = gearshifter;
        }

        public void Start()
        {
            var listener = new RpmChangedListener(_gearshifter);
            _eventBus.Attach(listener);
        }
    }
}
