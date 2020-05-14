using System;
using System.Threading.Tasks;
using GearboxDriver.Hardware.API;
using GearboxDriver.PublishedLanguage.Gearbox;
using GearboxDriver.PublishedLanguage.Pedals;
using GearboxDriver.PublishedLanguage.Transmission;
using GearboxDriver.Seedwork;

namespace GearboxDriver.SampleApplication.Demo
{
    // Class only for application demonstration and is out of scope of the model, contains very naive implementations
    class EngineEmulator : IEventListener
    {
        private readonly ExternalSystems _systems;
        private PedalPressure GasPressure { get; set; }
        private PedalPressure BrakePressure { get; set; }
        private Gear CurrentGear { get; set; }

        public EngineEmulator(ExternalSystems systems)
        {
            _systems = systems;
            GasPressure = new PedalPressure(0);
            BrakePressure = new PedalPressure(0);
            CurrentGear = new Gear(0);

            Task.Run(Run);
        }

        // Supposed to be very simple and naive
        public void HandleEvent(IEvent @event)
        {
            switch (@event)
            {
                case GasPressureChanged gasPressureChanged:
                    GasPressure = gasPressureChanged.PedalPressure;
                    return;
                case BrakePressureChanged brakePressureChanged:
                    BrakePressure = brakePressureChanged.PedalPressure;
                    return;
                case GearChanged gearChanged:
                    CurrentGear = gearChanged.EnteredGear;

                    if (gearChanged.EnteredGear.Value > gearChanged.PreviousGear.Value && _systems.getCurrentRpm() > 1200)
                        _systems.setCurrentRpm(Math.Max(_systems.getCurrentRpm() - 500, 0));

                    if (gearChanged.EnteredGear.Value < gearChanged.PreviousGear.Value)
                        _systems.setCurrentRpm(Math.Max(_systems.getCurrentRpm() + 200, 0));

                    break;
                case DriveModeEntered _:
                    _systems.setCurrentRpm(1000);
                    break;
            }
        }

        private void Run()
        {
            while (true)
            {
                var gasIncrease =  GetRpmIncreaseForGasPressure(GasPressure);
                var brakeDecrease = BrakePressure.Value * 30 * (_systems.getCurrentRpm() / 5000);
                var targetRpm = Math.Max(Math.Min(_systems.getCurrentRpm() + gasIncrease - brakeDecrease, 15000), 0);

                if (CurrentGear.Value == 1 && targetRpm < 1000)
                    targetRpm = 1000;

                _systems.setCurrentRpm(targetRpm);

                Task.Delay(TimeSpan.FromMilliseconds(25)).Wait();
            }
        }


        private double GetRpmIncreaseForGasPressure(PedalPressure pressure)
        {
            switch (CurrentGear.Value)
            {
                case 0:
                    return pressure.Value * 35;
                case 1:
                    return pressure.Value * 30 - 2 * (_systems.getCurrentRpm() / 5000);
                case 2: 
                    return pressure.Value * 25 - 2 * (_systems.getCurrentRpm()/5000);
                case 3: 
                    return pressure.Value * 20 - 3 * (_systems.getCurrentRpm() / 5000);
                case 4: 
                    return pressure.Value * 15 - 4 * (_systems.getCurrentRpm() / 5000);
                case 5:
                    return pressure.Value * 10 - 5 * (_systems.getCurrentRpm() / 5000);
                case 6:
                    return pressure.Value * 5 - 6 * (_systems.getCurrentRpm() / 5000);
            }

            return 0;
        }
    }
}
