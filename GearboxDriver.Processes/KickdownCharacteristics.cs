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
        public KickdownAction GetActionFor(ResponsivenessMode responsivenessMode, PedalPressure pedalPressure)
        {
            switch(responsivenessMode)
            {
                case ResponsivenessMode.Comfort:
                    if (pedalPressure.Value > 0.5d)
                        return KickdownAction.Singular;
                    break;
                case ResponsivenessMode.Sport:
                    if (pedalPressure.Value >= 0.7d)
                        return KickdownAction.Singular;
                    else if (pedalPressure.Value > 0.9d)
                        return KickdownAction.Double;
                    break;
            }

            return KickdownAction.None;
        }
    }
}
