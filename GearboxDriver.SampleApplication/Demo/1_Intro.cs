using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;
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

namespace GearboxDriver.SampleApplication.Demo
{
    public class Intro
    {
        public static void Play()
        {
            //PlayIntroduction();
            PlayFirstScenario();

        }

        public static void PlayIntroduction()
        {
            TimeHelper.PlayMessage("Welcome to the Overkill Gearbox Driver implementation demo.", 3);
            TimeHelper.PlayMessage("This software has been written by Michał Wityk (3 yrs of experience) and Maciej Białobrzeski (1,5 yrs of experience)", 0);
            TimeHelper.PlayMessage("as an entry for the DevUpgrade competition organized by BOTTEGA IT MINDS company.\n", 5);
            TimeHelper.PlayMessage("You are now going to see a few test scenarios of our gearbox in action.", 3);
            TimeHelper.PlayMessage("Feel free to traverse the code-behind after seeing the show.", 3);
            TimeHelper.PlayMessage("We are thankful for your time spent on reviewing our solution. \n", 2);
        }

        public static void PlayFirstScenario()
        {
            TimeHelper.PlayMessage("Our driver will enter the car now.", 3);
            TimeHelper.PlayMessage("He is going to setup Comfort Mode, slowly accelerate and then decelerate.", 3);
            TimeHelper.PlayMessage("You will the have chance to observe the changes in the gears.", 5);

            var eventBus = new EventBusThatYouDontWantToUseInProduction();
            var externalSystems = new ExternalSystems();
            var externalSystemsAdapter = new ExternalSystemsAdapter(externalSystems);
            var gearbox = new Gearbox();
            var gearboxAdapter = new GearboxAdapter(gearbox);
            var automaticGearshifter = new AutomaticGearshifter(gearboxAdapter);
            eventBus.Attach(new EventLogger());
            eventBus.Attach(new EngineEmulator(externalSystems));

            new AntiCorruptionLayerStartup(eventBus, externalSystemsAdapter, gearbox).Start();
            new GearshiftStartup(eventBus, automaticGearshifter).Start();
            new ProcessesStartup(eventBus, new GearshiftService(new Negotiator(), automaticGearshifter), new EngineCharacteristics()).Start();
            TimeHelper.WaitSeconds(3);
            var cabinService = new CabinService(eventBus);
            cabinService.SetDriveMode();
            TimeHelper.WaitSeconds(4);
            cabinService.SetResponsivenessMode(ResponsivenessMode.Economic);
            TimeHelper.WaitSeconds(4);
            cabinService.ApplyGasPedalPressure(new PedalPressure(0.15));
            TimeHelper.WaitSeconds(3);
            cabinService.ApplyGasPedalPressure(new PedalPressure(0.35));
            TimeHelper.WaitSeconds(3);
            cabinService.ApplyGasPedalPressure(new PedalPressure(0.55));
            TimeHelper.WaitSeconds(3);
            cabinService.ApplyGasPedalPressure(new PedalPressure(0.75));
            TimeHelper.WaitSeconds(3);
            cabinService.ApplyGasPedalPressure(new PedalPressure(0.55));
            TimeHelper.WaitSeconds(3);
            cabinService.ApplyGasPedalPressure(new PedalPressure(0.35));
            TimeHelper.WaitSeconds(3);
            cabinService.ApplyGasPedalPressure(new PedalPressure(0));
            TimeHelper.WaitSeconds(3);
            cabinService.ApplyBrakePedalPressure(new PedalPressure(1.0));
            TimeHelper.WaitSeconds(10);

            eventBus.Kill();
        }
    }
}
