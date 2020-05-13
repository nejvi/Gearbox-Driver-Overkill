using System;
using System.Collections.Generic;
using System.Text;
using GearboxDriver.Gearshift;
using GearboxDriver.Seedwork;

namespace GearboxDriver.Processes
{
    public class ProcessesStartup
    {
        private readonly IGearshiftService _gearshiftService;
        private readonly IEventBus _eventBus;
        private readonly Characteristics _engineCharacteristics;

        public ProcessesStartup(IEventBus eventBus, IGearshiftService gearshiftService, Characteristics engineCharacteristics)
        {
            _gearshiftService = gearshiftService;
            _engineCharacteristics = engineCharacteristics;
            _eventBus = eventBus;
        }

        public void Start()
        {
            var pool = new ProcessPool();
            pool.Add(new SmoothBrakingWithTrailerAttached(_gearshiftService));
            pool.Add(new GearYieldedWithKickdownActivated());
            pool.Add(new NoInterferenceToGearshiftWithManualMode());
            pool.Add(new GearboxDriverYielededWithMDynamicModeActivated(_gearshiftService));
            pool.Add(new RpmBasedByModes(_gearshiftService, _engineCharacteristics));
            _eventBus.Attach(pool);
        }
    }
}
