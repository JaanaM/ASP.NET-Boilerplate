using Abp.Events.Bus;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Domain.Events
{
    public class DomainEvents
    {
        public static IEventBus EventBus { get; set; }

        static DomainEvents()
        {
            EventBus = Abp.Events.Bus.EventBus.Default;
        }
    }
}
