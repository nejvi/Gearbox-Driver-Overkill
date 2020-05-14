using GearboxDriver.Gearshift;
using GearboxDriver.Hardware.ACL;
using GearboxDriver.PublishedLanguage.MDynamic;
using GearboxDriver.PublishedLanguage.VehicleMotion;
using GearboxDriver.Seedwork;

namespace GearboxDriver.Processes
{
    public class MDynamicSlippingDetectionProcess : IProcess
    {
        private readonly IGearshiftService _service;
        private bool MDynamicActive { get; set; }

        public MDynamicSlippingDetectionProcess(IGearshiftService service)
        {
            _service = service;
            MDynamicActive = false;
        }
        
        public void ApplyEvent(IEvent @event)
        {
            switch (@event)
            {
                case MDynamicModeEntered _:
                    MDynamicActive = true;
                    break;
                case MDynamicModeExited _:
                    MDynamicActive = false;
                    break;
                case VehicleStartedSlipping _:
                    if(MDynamicActive)
                        _service.AbstainFromChangingGears();
                    break;
                case VehicleStoppedSlipping _:
                    if (MDynamicActive)
                        _service.StopAbstainingFromChangingGears();
                    break;
            }
        }
    }
}
