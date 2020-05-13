using System;
using System.Collections.Generic;
using System.Text;
using GearboxDriver.Cabin.Pedals;
using GearboxDriver.Cabin.Responsiveness;
using GearboxDriver.Cabin.Transmission;
using GearboxDriver.Hardware.ACL;
using GearboxDriver.Seedwork;

namespace GearboxDriver.Cabin
{
    public class CabinService
    {
        private readonly IEventBus _eventBus;
        private readonly PedalPanel _pedalPanel;
        private readonly Lever _lever;
        private readonly ResponsivenessModeSelector _selector;

        public CabinService(IEventBus eventBus)
        {
            _eventBus = eventBus;
            _selector = new ResponsivenessModeSelector();
            _pedalPanel = new PedalPanel();
            _lever = new Lever();
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

        public void SetDriveMode()
        {
            _eventBus.SendEvent(_lever.EnterDriveMode());
        }

        public void SetResponsivenessMode(ResponsivenessMode mode)
        {
            switch (mode)
            {
                case ResponsivenessMode.Comfort:
                    _eventBus.SendEvent(_selector.EnterComfort());
                    break;
                case ResponsivenessMode.Sport:
                    _eventBus.SendEvent(_selector.EnterSport());
                    break;
                case ResponsivenessMode.Economic:
                    _eventBus.SendEvent(_selector.EnterEconomic());
                    break;
            }
        }

        public void SetAggressivenessLevel(AggressivenessLevel level)
        {
            _eventBus.SendEvent(_selector.SetAggressivenessLevel(level));
        }
    }
}
