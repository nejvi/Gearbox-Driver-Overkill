﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GearboxDriver.Hardware.ACL
{
    public class AngularSpeed : ValueObject
    {
        public double Value { get; }

        public AngularSpeed(double value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}