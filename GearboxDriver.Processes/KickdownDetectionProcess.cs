using GearboxDriver.Cabin.Pedals;
using GearboxDriver.Cabin.Responsiveness;
using GearboxDriver.Seedwork;

namespace GearboxDriver.Processes
{
    public class KickdownDetectionProcess : IProcess
    {
        private bool KickdownActivated { get; set; }
        private bool StrongKickdownActivated { get; set; }
        private ResponsivenessMode ResponsivenessMode { get; set; }

        public KickdownDetectionProcess()
        {
            KickdownActivated = false;
            StrongKickdownActivated = false;
            ResponsivenessMode = ResponsivenessMode.Economic;
        }

        public void ApplyEvent(IEvent @event)
        {
            switch (@event)
            {
                case GasPressureChanged gasPressure:
                    switch (ResponsivenessMode)
                    {
                        case ResponsivenessMode.Comfort:
                            {
                                if (gasPressure.PedalPressure.Pressure > 0.5d)
                                    KickdownActivated = true;
                                else
                                {
                                    if(KickdownActivated)
                                    {
                                        KickdownActivated = false;
                                    }
                                }
                            }
                            break;
                        case ResponsivenessMode.Sport:
                            {
                                if (gasPressure.PedalPressure.Pressure >= 0.7d)
                                    KickdownActivated = true;
                                else if (gasPressure.PedalPressure.Pressure > 0.9d) // Not delivered info
                                {
                                    KickdownActivated = false;
                                    StrongKickdownActivated = true;
                                }
                                else
                                {
                                    if (KickdownActivated)
                                        KickdownActivated = false;
                                    else if (StrongKickdownActivated)
                                        StrongKickdownActivated = false;
                                }
                            }
                            break;
                    }
                    break;
                case EconomicModeEntered _:
                    ResponsivenessMode = ResponsivenessMode.Economic;
                    break;
                case ComfortModeEntered _:
                    ResponsivenessMode = ResponsivenessMode.Comfort;
                    break;
                case SportModeEntered _:
                    ResponsivenessMode = ResponsivenessMode.Sport;
                    break;
            }
        }
    }
}
