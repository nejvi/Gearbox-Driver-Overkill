using System;
using System.Threading.Tasks;
using GearboxDriver.CabinControls;
using GearboxDriver.Gearshift;
using GearboxDriver.Gearshift.Negotiation;
using GearboxDriver.Gearshift.Shifting;
using GearboxDriver.Hardware.ACL;
using GearboxDriver.Hardware.API;
using GearboxDriver.Processes;
using GearboxDriver.PublishedLanguage.Pedals;
using GearboxDriver.PublishedLanguage.Responsiveness;

namespace GearboxDriver.SampleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var externalSystems = new ExternalSystems();
            var eventBus = new EventBusThatYouDontWantToUseInProduction();
            var externalSystemsAdapter = new ExternalSystemsAdapter(externalSystems);
            var gearbox = new Gearbox();
            var gearboxAdapter = new GearboxAdapter(gearbox);
            var automaticGearshifter = new AutomaticGearshifter(gearboxAdapter);
            eventBus.Attach(new EventLogger());
            eventBus.Attach(new EngineEmulator(externalSystems));

            new AntiCorruptionLayerStartup(eventBus, externalSystemsAdapter, gearbox).Start();
            new GearshiftStartup(eventBus, automaticGearshifter).Start();
            new ProcessesStartup(eventBus, new GearshiftService(new Negotiator(), automaticGearshifter), new EngineCharacteristics()).Start();

            var cabinService = new CabinService(eventBus);
            cabinService.SetDriveMode();
            cabinService.SetResponsivenessMode(ResponsivenessMode.Sport);
            cabinService.SetAggressivenessLevel(AggressivenessLevel.Third);
            cabinService.ApplyGasPedalPressure(new PedalPressure(0.75));
            Task.Delay(TimeSpan.FromSeconds(10)).Wait();
            cabinService.ApplyGasPedalPressure(new PedalPressure(0.5));
            Task.Delay(TimeSpan.FromSeconds(10)).Wait();
            cabinService.ApplyGasPedalPressure(new PedalPressure(0.0));
            Task.Delay(TimeSpan.FromSeconds(5)).Wait();
            cabinService.ApplyBrakePedalPressure(new PedalPressure(1.0));

            Console.ReadKey();
        }
    }
}
