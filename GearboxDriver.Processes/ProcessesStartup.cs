using GearboxDriver.Gearshift;
using GearboxDriver.Seedwork;

namespace GearboxDriver.Processes
{
    public class ProcessesStartup
    {
        private readonly IGearshiftService _gearshiftService;
        private readonly IEventBus _eventBus;
        private readonly EngineCharacteristics _engineCharacteristics;
        private readonly KickdownCharacteristics _kickdownCharacteristics;

        public ProcessesStartup(IEventBus eventBus, IGearshiftService gearshiftService, EngineCharacteristics engineCharacteristics, KickdownCharacteristics kickdownCharacteristics)
        {
            _gearshiftService = gearshiftService;
            _engineCharacteristics = engineCharacteristics;
            _kickdownCharacteristics = kickdownCharacteristics;
            _eventBus = eventBus;
        }

        public void Start()
        {
            var pool = new ProcessPool();
            pool.Add(new TrailerEngineBrakingDetectionProcess(_gearshiftService));
            pool.Add(new KickdownDetectionProcess(_kickdownCharacteristics, _gearshiftService));
            pool.Add(new ManualModeProgramUpdatingProcess(_gearshiftService));
            pool.Add(new MDynamicSlippingDetectionProcess(_gearshiftService));
            pool.Add(new ResponsivenessModeProgramUpdatingProcess(_gearshiftService, _engineCharacteristics));
            pool.Add(new ExhaustExplosionDetectionProcess(_eventBus));
            _eventBus.Attach(pool);
        }
    }
}
