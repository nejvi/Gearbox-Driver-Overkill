using GearboxDriver.Seedwork;
using System.Collections.Generic;

namespace GearboxDriver.Gearshift
{
    public class Gear : ValueObject
    {
        public int Value { get; }

        public Gear(int value)
        {
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
    }
}
