using GearboxDriver.Gearshift;
using GearboxDriver.Seedwork;

namespace GearboxDriver.Processes
{
    public class ProcessesStartup
    {
        private readonly IGearshiftService _gearshiftService;
        private readonly IEventBus _eventBus;
        private readonly EngineCharacteristics _engineCharacteristics;

        public ProcessesStartup(IEventBus eventBus, IGearshiftService gearshiftService, EngineCharacteristics engineCharacteristics)
        {
            _gearshiftService = gearshiftService;
            _engineCharacteristics = engineCharacteristics;
            _eventBus = eventBus;
        }

        public void Start()
        {
            var pool = new ProcessPool();
            pool.Add(new TrailerEngineBrakingDetectionProcess(_gearshiftService));
            //pool.Add(new KickdownDetectionProcess());
            pool.Add(new ManualModeProgramUpdatingProcess(_gearshiftService));
            pool.Add(new MDynamicSlippingDetectionProcess(_gearshiftService));
            pool.Add(new ResponsivenessModeProgramUpdatingProcess(_gearshiftService, _engineCharacteristics));
            _eventBus.Attach(pool);
        }
    }
}
