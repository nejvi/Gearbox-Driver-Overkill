using System;
using System.Threading;
using System.Threading.Tasks;
using GearboxDriver.Gearshift;
using GearboxDriver.Seedwork;
using GearboxDriver.PublishedLanguage.Gearbox;
using GearboxDriver.PublishedLanguage.ManualGearshifting;

namespace GearboxDriver.Processes
{
    // When not in manual mode, given that the user changes the gear manually, the gear should come back to the automatic one after some time
    // (we kind of were going out of time when implementing this one though)
    public class TemporalManualControlProcess : IProcess
    {
        private bool VehicleInManualMode { get; set; }
        private Gear CurrentGear { get; set; }
        private Gear SelectedGear { get; set; }
        private CancellationTokenSource _tokenSource;
        private readonly IGearshiftService _service;
        private readonly TimeSpan _comebackDelay = TimeSpan.FromSeconds(5);

        public TemporalManualControlProcess(IGearshiftService service)
        {
            _service = service;
            _tokenSource = new CancellationTokenSource();
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
                    _service.StopTargetingAnyGear();
                    _tokenSource.Cancel();
                    break;
                case GearUpshiftedManually _:
                    if (VehicleInManualMode)
                        break;

                    SelectedGear = SelectedGear.UpshiftedBy(new Gear(1));
                    _service.TargetGear(SelectedGear);
                    RescheduleComebackTimer();
                    break;
                case GearDownshiftedManually _:
                    if (VehicleInManualMode)
                        break;

                    SelectedGear = SelectedGear.DownshiftedBy(new Gear(1));
                    _service.TargetGear(SelectedGear);
                    RescheduleComebackTimer();
                    break;
            }
        }

        private void RescheduleComebackTimer()
        {
            _tokenSource?.Cancel();
            _tokenSource = new CancellationTokenSource();
            Task.Delay(_comebackDelay, _tokenSource.Token).ContinueWith(t => { ComeBackToAutomatic(); });
        }

        private void ComeBackToAutomatic()
        {
            _service.StopTargetingAnyGear();
            SelectedGear = CurrentGear;
        }

    }
}
