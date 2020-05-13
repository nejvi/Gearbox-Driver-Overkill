using GearboxDriver.Hardware.ACL.Runtime;
using System;
using System.Collections.Generic;
using System.Text;
using GearboxDriver.Seedwork;

namespace GearboxDriver.Hardware.ACL.VehicleSlippingReporting
{
    public class SlippingReporter : IReporter
    {
        private readonly IEventBus _eventBus;
        private readonly ISlippingProvider _angularProvider;
        private bool LastReportedSlipping { get; set; }
        private bool isEverReported { get; set; }

        public SlippingReporter(IEventBus eventBus, ISlippingProvider slippingProvider)
        {
            _eventBus = eventBus;
            _angularProvider = slippingProvider;
        }

        public void TryToReport()
        {
            var currentSlipping = _angularProvider.IsCurrentlySlipping();
            if (LastReportedSlipping == currentSlipping && isEverReported) // VehicleStartedSlipping, VehicleStoppedSlipping
                return;

            if (currentSlipping)
                _eventBus.SendEvent(new VehicleStartedSlipping());
            else
                _eventBus.SendEvent(new VehicleStoppedSlipping());

            LastReportedSlipping = currentSlipping;
            isEverReported = true;
        }
    }
}
