﻿using GearboxDriver.Gearshift;
using GearboxDriver.Hardware.ACL;
using GearboxDriver.PublishedLanguage.Towing;
using GearboxDriver.Seedwork;

namespace GearboxDriver.Processes
{
    public class TrailerEngineBrakingDetectionProcess : IProcess
    {
        private bool HookOccupied { get; set; }
        private bool CarMovingDownhill { get; set; }
        private Gear CurrentGear { get; set; }
        private bool RequestedEngineBraking { get; set; }
        private readonly IGearshiftService _service;

        public TrailerEngineBrakingDetectionProcess(IGearshiftService service)
        {
            _service = service;
            CarMovingDownhill = false;
            CurrentGear = new Gear(0);
            HookOccupied = false;
            CarMovingDownhill = false;
        }

        public void ApplyEvent(IEvent @event)
        {
            switch (@event)
            {
                case TrailerHookBecameOccupied _:
                    HookOccupied = true;
                    break;
                case TrailerHookStoppedBeingOccupied _:
                    HookOccupied = false;
                    break;
                //case GearUpshifted _: // todo
                //case GearDownshifted _: // todo
                case VehicleTiltedDownhill _:
                    CarMovingDownhill = true;
                    _service.TargetGear(CurrentGear.DownshiftedBy(new Gear(1)));
                    break;
                case VehicleTiledToStraightPosition _:
                case VehicleTiltedUphill _:
                    CarMovingDownhill = false;
                    break;
            }

            Act();
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
            RequestedEngineBraking = true;
        }
    }
}
