using GearboxDriver.Hardware.ACL.RpmReporting;
using GearboxDriver.Hardware.ACL.Runtime;
using GearboxDriver.Hardware.ACL.TiltPositionReporting;
using GearboxDriver.Hardware.ACL.VehicleSlippingReporting;
using GearboxDriver.Seedwork;

namespace GearboxDriver.Hardware.ACL
{
    public class AntiCorruptionLayerStartup
    {
        private readonly IEventBus _eventBus;
        private readonly ExternalSystemsAdapter _adapter;

        public AntiCorruptionLayerStartup(IEventBus eventBus, ExternalSystemsAdapter adapter)
        {
            _eventBus = eventBus;
            _adapter = adapter;
        }

        public void Start()
        {
            var builder = new EngineBuilder()
                .AddReporter(new RPMReporter(_eventBus, _adapter))
                .AddReporter(new SlippingReporter(_eventBus, _adapter));

            if (_adapter.SupportsTiltPosition())
                builder.AddReporter(new TiltChangeReporter(_eventBus, _adapter));

            builder.Build().Start();
        }
    }
}
