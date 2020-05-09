using System;
using System.Collections.Generic;
using System.Text;

namespace GearboxDriver.Seedwork
{
    public class DomainRuleViolatedException : Exception
    {
        public DomainRuleViolatedException(string? message) : base(message)
        {
        }
    }
}
