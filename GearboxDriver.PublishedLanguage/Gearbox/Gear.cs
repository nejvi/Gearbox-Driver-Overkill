using System;
using System.Collections.Generic;
using GearboxDriver.Seedwork;

namespace GearboxDriver.PublishedLanguage.Gearbox
{
    public class Gear : ValueObject
    {
        public int Value { get; }

        public Gear(int value)
        {
            if (Value < 0)
                throw new Exception("Gear number must be a positive value");

            Value = value;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public Gear DownshiftedBy(Gear gear)
        {
            return new Gear(Value - gear.Value);
        }

        public Gear UpshiftedBy(Gear gear)
        {
            return new Gear(Value + gear.Value);
        }
    }
}
