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
            eventBus.Attach(new EventLogger());
            eventBus.Attach(new EngineEmulator(externalSystems));

            new AntiCorruptionLayerStartup(eventBus, externalSystemsAdapter).Start();
            new ProcessesStartup(eventBus, new GearshiftService(new Negotiator(), new AutomaticGearshifter(null /*todo*/)), new Characteristics()).Start();

            var cabinService = new CabinService(eventBus);
            cabinService.SetDriveMode();
            cabinService.SetResponsivenessMode(ResponsivenessMode.Sport);
            cabinService.SetAggressivenessLevel(AggressivenessLevel.Third);
            cabinService.ApplyGasPedalPressure(new PedalPressure(0.75));
            Task.Delay(TimeSpan.FromSeconds(10)).Wait();
            cabinService.ApplyGasPedalPressure(new PedalPressure(0.5));
            Task.Delay(TimeSpan.FromSeconds(20)).Wait();
            cabinService.ApplyGasPedalPressure(new PedalPressure(0.0));
            Task.Delay(TimeSpan.FromSeconds(15)).Wait();
            cabinService.ApplyBrakePedalPressure(new PedalPressure(1.0));
            Task.Delay(TimeSpan.FromSeconds(3)).Wait();

            Console.ReadKey();
        }
    }
}
