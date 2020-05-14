using GearboxDriver.PublishedLanguage;
using GearboxDriver.PublishedLanguage.Pedals;
using GearboxDriver.PublishedLanguage.Responsiveness;
using System;
using System.Collections.Generic;
using System.Text;

namespace GearboxDriver.Processes
{
    public class KickdownCharacteristics : IKickdownCharacteristics
    {
        private const double DoubleKickdownThresholdInSportsMode = 0.9d; // no info provided
        private const double SingularKickdownThresholdInSportsMode = 0.7d;
        private const double SingularKickdownThresholdInComfortMode = 0.5d;

        public SuggestedKickdownAction GetActionFor(ResponsivenessMode responsivenessMode, PedalPressure pedalPressure)
        {
            switch(responsivenessMode)
            {
                case ResponsivenessMode.Comfort:
                    if (pedalPressure.Value > SingularKickdownThresholdInComfortMode)
                        return SuggestedKickdownAction.Singular;
                    break;
                case ResponsivenessMode.Sport:
                    if (pedalPressure.Value >= SingularKickdownThresholdInSportsMode)
                        return SuggestedKickdownAction.Singular;
                    else if (pedalPressure.Value > DoubleKickdownThresholdInSportsMode)
                        return SuggestedKickdownAction.Double;
                    break;
            }

            return SuggestedKickdownAction.None;
        }
    }
}
