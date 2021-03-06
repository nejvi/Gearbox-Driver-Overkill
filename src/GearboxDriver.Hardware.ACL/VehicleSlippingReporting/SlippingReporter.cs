﻿using GearboxDriver.Hardware.ACL.Runtime;
using GearboxDriver.PublishedLanguage.VehicleMotion;
using GearboxDriver.Seedwork;

namespace GearboxDriver.Hardware.ACL.VehicleSlippingReporting
{
    public class SlippingReporter : IReporter
    {
        private readonly IEventBus _eventBus;
        private readonly ISlippingSensor _angularSensor;
        private bool LastReportedSlipping { get; set; }

        public SlippingReporter(IEventBus eventBus, ISlippingSensor slippingSensor)
        {
            _eventBus = eventBus;
            _angularSensor = slippingSensor;
        }

        public void TryToReport()
        {
            var currentSlipping = _angularSensor.IsCurrentlySlipping();
            if (LastReportedSlipping == currentSlipping) // VehicleStartedSlipping, VehicleStoppedSlipping
                return;

           if (currentSlipping)
              _eventBus.SendEvent(new VehicleStartedSlipping());
           else
              _eventBus.SendEvent(new VehicleStoppedSlipping());

            LastReportedSlipping = currentSlipping;
        }
    }
}
