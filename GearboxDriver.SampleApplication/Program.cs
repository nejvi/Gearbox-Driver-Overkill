using System;
using GearboxDriver.Cabin;
using GearboxDriver.Cabin.Pedals;
using GearboxDriver.Cabin.Responsiveness;
using GearboxDriver.Gearshift;
using GearboxDriver.Gearshift.Negotiation;
using GearboxDriver.Gearshift.Shifting;
using GearboxDriver.Hardware.ACL;
using GearboxDriver.Hardware.API;
using GearboxDriver.Processes;
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
            eventBus.Attach(new EventLogger());

            new AntiCorruptionLayerStartup(eventBus, externalSystemsAdapter).Start();
            new ProcessesStartup(eventBus, new GearshiftService(new Negotiator(), new AutomaticGearshifter(null /*todo*/)), new Characteristics()).Start();

            var cabinService = new CabinService(eventBus);
            cabinService.SetDriveMode();
            cabinService.SetResponsivenessMode(ResponsivenessMode.Sport);
            cabinService.SetAggressivenessLevel(AggressivenessLevel.Third);
            cabinService.ApplyGasPedalPressure(new PedalPressure(0.75));


            Console.ReadKey();
        }
    }
}
