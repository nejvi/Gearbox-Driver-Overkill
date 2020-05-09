using System;
using System.Collections.Generic;
using System.Text;

namespace GearboxDriver.Hardware.ACL.Runtime
{
    public class EngineStartUpException : Exception
    {
        public EngineStartUpException(string message) : base(message)
        {
            
        }
    }
}
