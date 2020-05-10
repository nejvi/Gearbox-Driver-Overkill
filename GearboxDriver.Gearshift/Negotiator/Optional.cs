using System;
using System.Collections.Generic;
using System.Text;

namespace GearboxDriver.Gearshift.Negotiator
{
    internal class Optional<T> where T : class
    {
        public T InnerValue { get; }
        public bool HasValue { get; }

        public Optional(T innerValue)
        {
            InnerValue = innerValue;
            HasValue = innerValue != null;
        }
    }
}
