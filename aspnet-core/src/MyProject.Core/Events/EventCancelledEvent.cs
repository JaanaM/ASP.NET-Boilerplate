using Abp.Events.Bus.Entities;
namespace MyProject.Events
{
    public class EventCancelledEvent : EntityEventData<Event>
    {
        public EventCancelledEvent(Event entity)
           : base(entity)
        {
        }
    }
}