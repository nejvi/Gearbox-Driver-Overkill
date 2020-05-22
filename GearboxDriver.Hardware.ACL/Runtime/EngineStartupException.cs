using System;

namespace GearboxDriver.Hardware.ACL.Runtime
{
    public class EngineStartUpException : Exception
    {
        public EngineStartUpException(string message) : base(message)
        {
            
        }
    }
}
