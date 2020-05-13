using GearboxDriver.Cabin.ManualGearshifting;
using GearboxDriver.Gearshift;
using GearboxDriver.Seedwork;
using GearboxDriver.PublishedLanguage.Gearbox;

namespace GearboxDriver.Processes
{
    public class ManualModeProgramUpdatingProcess : IProcess
    {
        private bool VehicleInManualMode { get; set; }
        private Gear CurrentGear { get; set; }
        private Gear SelectedGear { get; set; }
        private readonly IGearshiftService _service;

        public ManualModeProgramUpdatingProcess(IGearshiftService service)
        {
            _service = service;
            VehicleInManualMode = false;
            CurrentGear = new Gear(0);
        }

        public void ApplyEvent(IEvent @event)
        {
            switch (@event)
            {
                case GearChanged gearChanged:
                    CurrentGear = gearChanged.EnteredGear;
                    break;
                case ManualGearshiftingModeEntered _:
                    VehicleInManualMode = true;

                    SelectedGear = CurrentGear;

                    _service.TargetGear(SelectedGear);
                    break;
                case ManualGearshiftingModeExited _:
                    VehicleInManualMode = false;

                    _service.StopTargetingAnyGear();
                    break;
                case GearUpshiftedManually _:
                    if (!VehicleInManualMode)
                        break;

                    SelectedGear = SelectedGear.UpshiftedBy(new Gear(1));
                    _service.TargetGear(SelectedGear);
                    break;
                case GearDownshiftedManually _:
                    if (!VehicleInManualMode)
                        break;

                    SelectedGear = SelectedGear.DownshiftedBy(new Gear(1));
                    _service.TargetGear(SelectedGear);
                    break;
            }
        }
    }
}
