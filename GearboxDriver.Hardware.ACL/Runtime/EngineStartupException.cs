using System;

namespace GearboxDriver.Hardware.ACL.Runtime
{
    public class EngineStartupException : Exception
    {
        public EngineStartupException(string message) : base(message)
        {
            
        }
    }
}
