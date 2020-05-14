using GearboxDriver.Gearshift.Shifting;
using GearboxDriver.Seedwork;

namespace GearboxDriver.Gearshift
{
    public class GearshiftStartup
    {
        private readonly IEventBus _eventBus;
        private readonly IGearbox _gearbox;

        public GearshiftStartup(IEventBus eventBus, IGearbox gearbox)
        {
            _eventBus = eventBus;
            _gearbox = gearbox;
        }

        public void Start()
        {
            var gearshifter = new AutomaticGearshifter(_gearbox);
            var listener = new RpmChangedListener(gearshifter);
            _eventBus.Attach(listener);
        }
    }
}
