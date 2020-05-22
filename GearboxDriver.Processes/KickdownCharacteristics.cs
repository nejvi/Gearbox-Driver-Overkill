using GearboxDriver.PublishedLanguage;
using GearboxDriver.PublishedLanguage.Gearbox;
using GearboxDriver.PublishedLanguage.Pedals;
using GearboxDriver.PublishedLanguage.Responsiveness;

namespace GearboxDriver.Processes
{
    public class KickdownCharacteristics : IKickdownCharacteristics
    {
        private const double DoubleKickdownThresholdInSportsMode = 0.9d; // no info provided
        private const double SingularKickdownThresholdInSportsMode = 0.7d;
        private const double SingularKickdownThresholdInComfortMode = 0.5d;
        private const double SingularKickdownRpmInComfortMode = 4500d;
        private const double SingularKickdownRpmInSportMode = 5000d;
        private const double DoubleKickdownRpmInSportMode = 5000d;

        public SuggestedKickdownAction GetActionFor(ResponsivenessMode responsivenessMode, PedalPressure pedalPressure, Rpm currentRpm)
        {
            switch(responsivenessMode)
            {
                case ResponsivenessMode.Comfort:
                    if (pedalPressure.Value > SingularKickdownThresholdInComfortMode && currentRpm.Value >= SingularKickdownRpmInComfortMode)
                        return SuggestedKickdownAction.Singular;
                    break;
                case ResponsivenessMode.Sport:
                    if (pedalPressure.Value >= DoubleKickdownThresholdInSportsMode && currentRpm.Value >= DoubleKickdownRpmInSportMode)
                        return SuggestedKickdownAction.Double;
                    else if (pedalPressure.Value >= SingularKickdownThresholdInSportsMode && currentRpm.Value >= SingularKickdownRpmInSportMode)
                        return SuggestedKickdownAction.Singular;
                    break;
            }

            return SuggestedKickdownAction.None;
        }
    }
}
