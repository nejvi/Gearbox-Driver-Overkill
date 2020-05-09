using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace GearboxDriver.Hardware.ACL
{
    public interface IEventBus
    {
        void SendEvent(IEvent @event);
    }
}
