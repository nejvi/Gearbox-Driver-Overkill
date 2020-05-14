using GearboxDriver.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace GearboxDriver.PublishedLanguage.SoundEffects
{
    public class ExhaustExplosionOccured : IEvent
    {
        public override string ToString()
        {
            return $"Exhaust explosion occured.";
        }
    }
}
