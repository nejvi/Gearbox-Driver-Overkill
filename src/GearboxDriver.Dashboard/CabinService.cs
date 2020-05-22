using GearboxDriver.PublishedLanguage.Pedals;
using GearboxDriver.PublishedLanguage.Responsiveness;
using GearboxDriver.Seedwork;

namespace GearboxDriver.CabinControls
{
    public class CabinService
    {
        private readonly IEventBus _eventBus;
        private readonly PedalPanel _pedalPanel;
        private readonly Lever _lever;
        private readonly TrailerHook _hook;
        private readonly ResponsivenessModeSelector _responsivenessModeSelector;
        private readonly ManualGearshift _manualGearshift;
        private readonly MDynamicModeSelector _mDynamicModeSelector;

        public CabinService(IEventBus eventBus)
        {
            _eventBus = eventBus;
            _mDynamicModeSelector = new MDynamicModeSelector();
            _responsivenessModeSelector = new ResponsivenessModeSelector();
            _pedalPanel = new PedalPanel();
            _manualGearshift = new ManualGearshift();
            _lever = new Lever();
            _hook = new TrailerHook();
        }

        public void ApplyGasPedalPressure(PedalPressure pressure)
        {
            _eventBus.SendEvent(_pedalPanel.ChangeGasPressure(pressure));
        }

        public void ApplyBrakePedalPressure(PedalPressure pressure)
        {
            _eventBus.SendEvent(_pedalPanel.ChangeBrakePressure(pressure));
        }

        public void SetParkMode()
        {
            _eventBus.SendEvent(_lever.EnterParkMode());
        }

        public void EnterDynamicMode()
        {
            _eventBus.SendEvent(_mDynamicModeSelector.Enable());
        }

        public void EnterManualMode()
        {
            _eventBus.SendEvent(_manualGearshift.EnterManualMode());
        }

        public void UpshiftManually()
        {
            _eventBus.SendEvent(_manualGearshift.Upshift());
        }

        public void DownshiftManually()
        {
            _eventBus.SendEvent(_manualGearshift.Downshift());
        }

        public void ExitManualMode()
        {
            _eventBus.SendEvent(_manualGearshift.ExitManualMode());
        }

        public void SetDriveMode()
        {
            _eventBus.SendEvent(_lever.EnterDriveMode());
        }

        public void SetResponsivenessMode(ResponsivenessMode mode)
        {
            switch (mode)
            {
                case ResponsivenessMode.Comfort:
                    _eventBus.SendEvent(_responsivenessModeSelector.EnterComfort());
                    break;
                case ResponsivenessMode.Sport:
                    _eventBus.SendEvent(_responsivenessModeSelector.EnterSport());
                    break;
                case ResponsivenessMode.Economic:
                    _eventBus.SendEvent(_responsivenessModeSelector.EnterEconomic());
                    break;
            }
        }

        public void SetAggressivenessLevel(AggressivenessLevel level)
        {
            _eventBus.SendEvent(_responsivenessModeSelector.SetAggressivenessLevel(level));
        }

        public void AttachTrailer()
        {
            _eventBus.SendEvent(_hook.AttachTrailer());
        }
    }
}
