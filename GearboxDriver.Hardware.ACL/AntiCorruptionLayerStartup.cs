using GearboxDriver.Hardware.ACL.CurrentGearReporting;
using GearboxDriver.Hardware.ACL.RpmReporting;
using GearboxDriver.Hardware.ACL.Runtime;
using GearboxDriver.Hardware.ACL.TiltPositionReporting;
using GearboxDriver.Hardware.ACL.VehicleSlippingReporting;
using GearboxDriver.Hardware.API;
using GearboxDriver.Seedwork;

namespace GearboxDriver.Hardware.ACL
{
    public class AntiCorruptionLayerStartup
    {
        private readonly IEventBus _eventBus;
        private readonly ExternalSystemsAdapter _adapter;
        private readonly Gearbox _gearbox;

        public AntiCorruptionLayerStartup(IEventBus eventBus, ExternalSystemsAdapter adapter, Gearbox gearbox)
        {
            _eventBus = eventBus;
            _adapter = adapter;
            _gearbox = gearbox;
        }

        public void Start()
        {
            var builder = new EngineBuilder()
                .AddReporter(new RpmReporter(_eventBus, _adapter))
                .AddReporter(new SlippingReporter(_eventBus, _adapter))
                .AddReporter(new GearReporter(_eventBus, new GearboxAdapter(_gearbox)));

            if (_adapter.SupportsTiltPosition())
                builder.AddReporter(new TiltChangeReporter(_eventBus, _adapter));

            builder.Build().Start();
        }
    }
}
