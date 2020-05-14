using GearboxDriver.PublishedLanguage.Gearbox;
using GearboxDriver.PublishedLanguage.ManualGearshifting;
using GearboxDriver.PublishedLanguage.Responsiveness;
using GearboxDriver.PublishedLanguage.SoundEffects;
using GearboxDriver.Seedwork;
using System;

namespace GearboxDriver.Processes
{
    public class ExhaustExplosionDetectionProcess : IProcess
    {
        private readonly IEventBus _eventBus;
        private AggressivenessLevel AggressivenessLevel { get; set; }

        public ExhaustExplosionDetectionProcess(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void ApplyEvent(IEvent @event)
        {
            switch(@event)
            {
                case AggressivenessLevelSelected aggressivenessLevel:
                    AggressivenessLevel = aggressivenessLevel.Level;
                    break;
                case GearChanged gearChanged:
                    if (gearChanged.EnteredGear.Value < gearChanged.PreviousGear.Value && AggressivenessLevel == AggressivenessLevel.Third)
                        _eventBus.SendEvent(new ExhaustExplosionOccured());
                    break;
            }
        }
    }
}
