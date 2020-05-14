﻿using GearboxDriver.PublishedLanguage;
using GearboxDriver.PublishedLanguage.Pedals;
using GearboxDriver.PublishedLanguage.Responsiveness;
using System;
using System.Collections.Generic;
using System.Text;

namespace GearboxDriver.Processes
{
    public interface IKickdownCharacteristics
    {
        KickdownAction GetActionFor(ResponsivenessMode responsivenessMode, PedalPressure pedalPressure);
    }
}
