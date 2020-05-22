using System;

namespace GearboxDriver.Seedwork
{
    public class DomainRuleViolatedException : Exception
    {
        public DomainRuleViolatedException(string? message) : base(message)
        {
        }
    }
}
