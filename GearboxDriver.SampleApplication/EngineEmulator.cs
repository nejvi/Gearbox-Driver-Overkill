using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GearboxDriver.Hardware.API;
using GearboxDriver.PublishedLanguage.Pedals;
using GearboxDriver.Seedwork;

namespace GearboxDriver.SampleApplication
{
    // Class only for demonstration, may be ugly
    class EngineEmulator : IEventListener
    {
        private readonly ExternalSystems _systems;
        private PedalPressure GasPressure { get; set; }
        private PedalPressure BrakePressure { get; set; }

        public EngineEmulator(ExternalSystems systems)
        {
            _systems = systems;
            GasPressure = new PedalPressure(0);
            BrakePressure = new PedalPressure(0);

            Task.Run(Run);
        }

        // Supposed to be very simple and naive
        public void HandleEvent(IEvent @event)
        {
            if (@event is GasPressureChanged gasPressureChanged)
            {
                GasPressure = gasPressureChanged.PedalPressure;
                return;
            }

            if (@event is BrakePressureChanged brakePressureChanged)
            {
                BrakePressure = brakePressureChanged.PedalPressure;
                return;
            }
        }

        private void Run()
        {
            while (true)
            {
                _systems.setCurrentRpm(Math.Min(_systems.getCurrentRpm() + GasPressure.Value * 10, 10000));
                _systems.setCurrentRpm(Math.Max(_systems.getCurrentRpm() - BrakePressure.Value * 10, 0));
                Task.Delay(TimeSpan.FromMilliseconds(30)).Wait();
            }
        }
    }
}
