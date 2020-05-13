using System;
using GearboxDriver.Cabin;
using GearboxDriver.Cabin.Pedals;
using GearboxDriver.Cabin.Responsiveness;
using GearboxDriver.Cabin.Transmission;
using GearboxDriver.Hardware.ACL;
using GearboxDriver.Hardware.API;
using GearboxDriver.SampleInfrastructure;

namespace GearboxDriver.SampleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var externalSystems = new ExternalSystems();
            var eventBus = new EventBusThatYouDontWantToUseInProduction();
            var externalSystemsAdapter = new ExternalSystemsAdapter(externalSystems);

            new AntiCorruptionLayerStartup(eventBus, externalSystemsAdapter).Start();

            var cabinService = new CabinService(eventBus);

            cabinService.SetDriveMode();
            cabinService.SetResponsivenessMode(ResponsivenessMode.Sport);
            cabinService.SetAggressivenessLevel(AggressivenessLevel.Third);
            cabinService.ApplyGasPedalPressure(new PedalPressure(0.75));

            Console.ReadKey();
        }
    }
}
