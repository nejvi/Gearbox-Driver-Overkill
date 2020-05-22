using GearboxDriver.Gearshift;
using GearboxDriver.PublishedLanguage.Towing;
using GearboxDriver.PublishedLanguage.VehicleMotion;
using GearboxDriver.Seedwork;

namespace GearboxDriver.Processes
{
    public class TrailerEngineBrakingDetectionProcess : IProcess
    {
        private bool HookOccupied { get; set; }
        private bool CarMovingDownhill { get; set; }
        private bool RequestedEngineBraking { get; set; }
        private readonly IGearshiftService _service;

        public TrailerEngineBrakingDetectionProcess(IGearshiftService service)
        {
            _service = service;
            CarMovingDownhill = false;
            HookOccupied = false;
            CarMovingDownhill = false;
        }

        public void ApplyEvent(IEvent @event)
        {
            UpdateState(@event);

            Act();
        }

        private void UpdateState(IEvent @event)
        {
            switch (@event)
            {
                case TrailerHookBecameOccupied _:
                    HookOccupied = true;
                    break;
                case TrailerHookStoppedBeingOccupied _:
                    HookOccupied = false;
                    break;
                case VehicleTiltedDownhill _:
                    CarMovingDownhill = true;
                    break;
                case VehicleTiledToStraightPosition _:
                case VehicleTiltedUphill _:
                    CarMovingDownhill = false;
                    break;
            }
        }

        private void Act()
        {
            if (RequestedEngineBraking && !ShouldAct)
                StopEngineBraking();

            if (!RequestedEngineBraking && ShouldAct)
                DoEngineBraking();
        }

        private bool ShouldAct => CarMovingDownhill && HookOccupied;

        private void DoEngineBraking()
        {
            _service.DoEngineBraking();
            RequestedEngineBraking = true;
        }

        private void StopEngineBraking()
        {
            _service.StopDoingEngineBraking();
            RequestedEngineBraking = false;
        }
    }
}
