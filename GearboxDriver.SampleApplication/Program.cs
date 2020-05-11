using System;
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

            Console.ReadKey();
        }
    }
}
