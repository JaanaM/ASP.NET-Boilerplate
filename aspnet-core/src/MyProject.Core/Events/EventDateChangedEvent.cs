﻿using Abp.Events.Bus.Entities;

namespace MyProject.Events
{
    public class EventDateChangedEvent : EntityEventData<Event>
    {
        public EventDateChangedEvent(Event entity)
           : base(entity)
        {
        }
    }
}