using GearboxDriver.PublishedLanguage.Gearbox;
using GearboxDriver.Seedwork;

namespace GearboxDriver.Gearshift.Shifting
{
    class RpmChangedListener : IEventListener
    {
        private readonly AutomaticGearshifter _automaticGearshifter;

        public RpmChangedListener(AutomaticGearshifter automaticGearshifter)
        {
            _automaticGearshifter = automaticGearshifter;
        }

        public void HandleEvent(IEvent @event)
        {
            if (@event is RpmChanged rpmChanged)
                _automaticGearshifter.HandleRpmChange(rpmChanged.NewRpm);
        }
    }
}
