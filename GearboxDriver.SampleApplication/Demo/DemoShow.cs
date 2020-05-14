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
using GearboxDriver.Seedwork;

namespace GearboxDriver.SampleApplication.Demo
{
    public class DemoShow
    {
        private static ExternalSystems _externalSystems;

        public static void Play()
        {
            PlayIntroduction();
            PlayEconomicScenario();
            PlaySportsScenario();
            PlaySportsAggressiveScenario();
            PlayMDynamicScenario();
            PlayTowingScenario();
            PlayKickdownScenario();
            PlayManualScenario();
            PlayAfterword();
        }

        public static void PlayIntroduction()
        {
            TimeHelper.PlayMessage("Welcome to the Overkill Gearbox Driver implementation demo.", 3);
            TimeHelper.PlayMessage("This software has been written by Michał Wityk (3 yrs of experience) and Maciej Białobrzeski (1,5 yrs of experience)", 0);
            TimeHelper.PlayMessage("as an entry for the DevUpgrade competition organized by BOTTEGA IT MINDS company.\n", 5);
            TimeHelper.PlayMessage("You are now going to see a few test scenarios of our gearbox in action.", 3);
            TimeHelper.PlayMessage("Feel free to traverse the code-behind after seeing the show.", 3);
            TimeHelper.PlayMessage("We are thankful for your time spent on reviewing our solution. \n", 2);
            TimeHelper.PlayMessage("(Please don't close the application until the end not to miss anything) \n", 2);
        }

        public static void PlayEconomicScenario()
        {
            TimeHelper.PlayMessage("Our driver will enter the car now.", 3);
            TimeHelper.PlayMessage("He is going to setup Comfort Mode, slowly accelerate and then decelerate.", 3);
            TimeHelper.PlayMessage("You will the have chance to observe the changes in the gears and Rpm being between 1000 - 2000.", 5);

            var eventBus = new EventBusThatYouDontWantToUseInProduction();
            SetupSystem(eventBus);
            var cabinService = new CabinService(eventBus);

            cabinService.SetDriveMode();
            TimeHelper.WaitSeconds(4);
            cabinService.SetResponsivenessMode(ResponsivenessMode.Economic);
            TimeHelper.WaitSeconds(4);

            DoCommonPedalOperations(cabinService);

            eventBus.Kill();
        }

        public static void PlaySportsScenario()
        {
            TimeHelper.PlayMessage("\n\nNow we will observe the same situation in sport mode.\n", 3);

            var eventBus = new EventBusThatYouDontWantToUseInProduction();
            SetupSystem(eventBus);
            var cabinService = new CabinService(eventBus);

            cabinService.SetDriveMode();
            TimeHelper.WaitSeconds(4);
            cabinService.SetResponsivenessMode(ResponsivenessMode.Sport);
            TimeHelper.WaitSeconds(4);

            DoCommonPedalOperations(cabinService);

            eventBus.Kill();
        }

        public static void PlaySportsAggressiveScenario()
        {
            TimeHelper.PlayMessage("\n\nLet's add third aggressive mode now! Observe the explosions from the exhaust when reducing!\n", 3);

            var eventBus = new EventBusThatYouDontWantToUseInProduction();
            SetupSystem(eventBus);
            var cabinService = new CabinService(eventBus);

            cabinService.SetDriveMode();
            TimeHelper.WaitSeconds(4);
            cabinService.SetResponsivenessMode(ResponsivenessMode.Sport);
            cabinService.SetAggressivenessLevel(AggressivenessLevel.Third);
            TimeHelper.WaitSeconds(4);

            DoCommonPedalOperations(cabinService);

            eventBus.Kill();
        }

        public static void PlayMDynamicScenario()
        {
            TimeHelper.PlayMessage("\n\nTime for MDynamic scenario. In the middle of the demo, the car will start slipping and gears won't change.\n", 3);

            var eventBus = new EventBusThatYouDontWantToUseInProduction();
            SetupSystem(eventBus);
            var cabinService = new CabinService(eventBus);

            cabinService.SetDriveMode();
            TimeHelper.WaitSeconds(4);
            cabinService.SetResponsivenessMode(ResponsivenessMode.Comfort);
            cabinService.EnterDynamicMode();
            TimeHelper.WaitSeconds(4);

            DoCommonPedalOperationsWithAngularSpeedPeakInTheMiddle(cabinService);

            eventBus.Kill();
        }

        public static void PlayTowingScenario()
        {
            TimeHelper.PlayMessage("\n\nTowing mode? Hold our beer. We will attach a trailer and the car will enter a hill in the middle of the demo.", 3);
            TimeHelper.PlayMessage("You can observe gear downshift as soon as the car tilts down. The gear will be restored along with the position.\n", 3);

            var eventBus = new EventBusThatYouDontWantToUseInProduction();
            SetupSystem(eventBus);
            var cabinService = new CabinService(eventBus);

            cabinService.SetDriveMode();
            TimeHelper.WaitSeconds(4);
            cabinService.SetResponsivenessMode(ResponsivenessMode.Comfort);
            cabinService.AttachTrailer();
            TimeHelper.WaitSeconds(4);

            DoCommonPedalOperationsWithLightsChangesInTheMiddle(cabinService);

            eventBus.Kill();
        }

        public static void PlayKickdownScenario()
        {
            TimeHelper.PlayMessage("\n\nTime for kickdown! The car will enter 5000 RPM. Then the driver will add more gas.", 3);
            TimeHelper.PlayMessage("At that point the car should drop 2 gears. The driver will reduce gas and the gears will upshift back.\n", 3);

            var eventBus = new EventBusThatYouDontWantToUseInProduction();
            SetupSystem(eventBus);
            var cabinService = new CabinService(eventBus);

            cabinService.SetDriveMode();
            TimeHelper.WaitSeconds(4);
            cabinService.SetResponsivenessMode(ResponsivenessMode.Sport);
            cabinService.SetAggressivenessLevel(AggressivenessLevel.Third);
            TimeHelper.WaitSeconds(4);

            DoAggressivePedalChangesToCauseKickdown(cabinService);

            eventBus.Kill();
        }

        public static void PlayManualScenario()
        {
            TimeHelper.PlayMessage("\n\nManual gearshifting scenario. The driver will turn on manual mode during the drive.", 3);
            TimeHelper.PlayMessage("He will keep switching the gears and then come back to automatic economic mode.\n", 3);

            var eventBus = new EventBusThatYouDontWantToUseInProduction();
            SetupSystem(eventBus);
            var cabinService = new CabinService(eventBus);

            cabinService.SetDriveMode();
            TimeHelper.WaitSeconds(4);
            cabinService.SetResponsivenessMode(ResponsivenessMode.Economic);
            TimeHelper.WaitSeconds(4);

            DoSomeManualDrivingInTheMiddle(cabinService);

            eventBus.Kill();
        }

        private static void DoCommonPedalOperations(CabinService cabinService)
        {
            cabinService.ApplyGasPedalPressure(new PedalPressure(0.15));
            TimeHelper.WaitSeconds(3);
            cabinService.ApplyGasPedalPressure(new PedalPressure(0.35));
            TimeHelper.WaitSeconds(3);
            cabinService.ApplyGasPedalPressure(new PedalPressure(0.55));
            TimeHelper.WaitSeconds(3);
            cabinService.ApplyGasPedalPressure(new PedalPressure(0.75));
            TimeHelper.WaitSeconds(5);
            cabinService.ApplyGasPedalPressure(new PedalPressure(0.55));
            TimeHelper.WaitSeconds(3);
            cabinService.ApplyGasPedalPressure(new PedalPressure(0.35));
            TimeHelper.WaitSeconds(3);
            cabinService.ApplyGasPedalPressure(new PedalPressure(0));
            TimeHelper.WaitSeconds(3);
            cabinService.ApplyBrakePedalPressure(new PedalPressure(0.5));
            TimeHelper.WaitSeconds(15);
        }

        private static void DoCommonPedalOperationsWithAngularSpeedPeakInTheMiddle(CabinService cabinService)
        {
            cabinService.ApplyGasPedalPressure(new PedalPressure(0.15));
            TimeHelper.WaitSeconds(3);
            cabinService.ApplyGasPedalPressure(new PedalPressure(0.35));
            _externalSystems.setAngularSpeed(500);
            TimeHelper.WaitSeconds(3);
            cabinService.ApplyGasPedalPressure(new PedalPressure(0.55));
            TimeHelper.WaitSeconds(3);
            cabinService.ApplyGasPedalPressure(new PedalPressure(0.75));
            TimeHelper.WaitSeconds(5);
            cabinService.ApplyGasPedalPressure(new PedalPressure(0.55));
            TimeHelper.WaitSeconds(3);
            _externalSystems.setAngularSpeed(100);
            cabinService.ApplyGasPedalPressure(new PedalPressure(0.35));
            TimeHelper.WaitSeconds(3);
            cabinService.ApplyGasPedalPressure(new PedalPressure(0));
            TimeHelper.WaitSeconds(3);
            cabinService.ApplyBrakePedalPressure(new PedalPressure(0.5));
            TimeHelper.WaitSeconds(5);
        }

        private static void DoCommonPedalOperationsWithLightsChangesInTheMiddle(CabinService cabinService)
        {
            cabinService.ApplyGasPedalPressure(new PedalPressure(0.15));
            TimeHelper.WaitSeconds(3);
            cabinService.ApplyGasPedalPressure(new PedalPressure(0.35));
            TimeHelper.WaitSeconds(3);
            cabinService.ApplyGasPedalPressure(new PedalPressure(0.55));
            TimeHelper.WaitSeconds(3);
            _externalSystems.getLights().setLightsPosition(3);
            cabinService.ApplyGasPedalPressure(new PedalPressure(0.75));
            TimeHelper.WaitSeconds(5);
            cabinService.ApplyGasPedalPressure(new PedalPressure(0.55));
            TimeHelper.WaitSeconds(3);
            _externalSystems.getLights().setLightsPosition(5);
            cabinService.ApplyGasPedalPressure(new PedalPressure(0.35));
            TimeHelper.WaitSeconds(3);
            cabinService.ApplyGasPedalPressure(new PedalPressure(0));
            TimeHelper.WaitSeconds(3);
            cabinService.ApplyBrakePedalPressure(new PedalPressure(0.5));
            TimeHelper.WaitSeconds(5);
        }

        private static void DoAggressivePedalChangesToCauseKickdown(CabinService cabinService)
        {
            cabinService.ApplyGasPedalPressure(new PedalPressure(0.15));
            TimeHelper.WaitSeconds(3);
            cabinService.ApplyGasPedalPressure(new PedalPressure(0.35));
            TimeHelper.WaitSeconds(3);
            cabinService.ApplyGasPedalPressure(new PedalPressure(0.55));
            TimeHelper.WaitSeconds(10);
            cabinService.ApplyGasPedalPressure(new PedalPressure(0.75));
            TimeHelper.WaitSeconds(5);
            cabinService.ApplyGasPedalPressure(new PedalPressure(0.55));
            TimeHelper.WaitSeconds(3);
            cabinService.ApplyGasPedalPressure(new PedalPressure(0.35));
            TimeHelper.WaitSeconds(3);
            cabinService.ApplyGasPedalPressure(new PedalPressure(0));
            TimeHelper.WaitSeconds(3);
            cabinService.ApplyBrakePedalPressure(new PedalPressure(0.5));
            TimeHelper.WaitSeconds(5);
        }

        private static void DoSomeManualDrivingInTheMiddle(CabinService cabinService)
        {
            cabinService.ApplyGasPedalPressure(new PedalPressure(0.15));
            TimeHelper.WaitSeconds(3);
            cabinService.ApplyGasPedalPressure(new PedalPressure(0.45));
            TimeHelper.WaitSeconds(5);
            cabinService.ApplyGasPedalPressure(new PedalPressure(0.65));
            TimeHelper.WaitSeconds(10);
            cabinService.EnterManualMode();
            cabinService.DownshiftManually();
            cabinService.DownshiftManually();
            cabinService.ApplyGasPedalPressure(new PedalPressure(0.95));
            TimeHelper.WaitSeconds(5);
            cabinService.ApplyGasPedalPressure(new PedalPressure(0.55));
            TimeHelper.WaitSeconds(3);
            cabinService.UpshiftManually();
            cabinService.ApplyGasPedalPressure(new PedalPressure(0.35));
            TimeHelper.WaitSeconds(3);
            cabinService.ExitManualMode();
            TimeHelper.WaitSeconds(10);
            cabinService.ApplyGasPedalPressure(new PedalPressure(0));
            TimeHelper.WaitSeconds(3);
            cabinService.ApplyBrakePedalPressure(new PedalPressure(0.5));
            TimeHelper.WaitSeconds(13);
        }

        public static void PlayAfterword()
        {
            TimeHelper.PlayMessage("Thank you for your participation in our demo.", 3);
            TimeHelper.PlayMessage("You may get to the code now!", 0);
        }


        private static void SetupSystem(IEventBus eventBus)
        {
            _externalSystems = new ExternalSystems();
            var externalSystemsAdapter = new ExternalSystemsAdapter(_externalSystems);
            var gearbox = new Gearbox();
            var gearboxAdapter = new GearboxAdapter(gearbox);
            var automaticGearshifter = new AutomaticGearshifter(gearboxAdapter);
            eventBus.Attach(new EventLogger());
            eventBus.Attach(new EngineEmulator(_externalSystems));
            new AntiCorruptionLayerStartup(eventBus, externalSystemsAdapter, gearbox).Start();
            new GearshiftStartup(eventBus, automaticGearshifter).Start();
            new ProcessesStartup(eventBus, new GearshiftService(new Negotiator(), automaticGearshifter), new EngineCharacteristics(), new KickdownCharacteristics()).Start();
            TimeHelper.WaitSeconds(3);
        }
    }
}
