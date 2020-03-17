using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.Events.Bus.Entities;
using Abp.Events.Bus.Handlers;
using Abp.Threading;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Identity;
using MyProject.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyProject.Events
{
    /// <summary>
    /// Domain events: define and trigger some domain specific events on some state changes in our application
    /// Defined EventCancelledEven, EventDateChangedEvent
    /// We handle these events and notify related users about these changes. Also, I handle EntityCreatedEventDate<Event> (which is a pre-defined ABP event and triggered automatically)

    /// </summary>
    public class EventUserEmailer :
        IEventHandler<EntityCreatedEventData<Event>>,
        IEventHandler<EventDateChangedEvent>,
        IEventHandler<EventCancelledEvent>,
        ITransientDependency
    {
        public ILogger Logger { get; set; }

        private readonly IEventManager _eventManager;
        private readonly UserManager _userManager;

        public EventUserEmailer(
            UserManager userManager,
            IEventManager eventManager)
        {
            _userManager = userManager;
            _eventManager = eventManager;

            Logger = NullLogger.Instance;
        }

        [UnitOfWork]
        public virtual void HandleEvent(EntityCreatedEventData<Event> eventData)
        {
            //TODO: Send email to all tenant users as a notification

            var users = _userManager
                .Users
                .Where(u => u.TenantId == eventData.Entity.TenantId)
                .ToList();

            foreach (var user in users)
            {
                var message = string.Format("Hey! There is a new event '{0}' on {1}! Want to register?", eventData.Entity.Title, eventData.Entity.Date);
                Logger.Debug(string.Format("TODO: Send email to {0} -> {1}", user.EmailAddress, message));
            }
        }
        //Triggered when date of an event changed.It's triggered in Event.ChangeDate method.
        public void HandleEvent(EventDateChangedEvent eventData)
        {
            //TODO: Send email to all registered users!

            var registeredUsers = AsyncHelper.RunSync(() => _eventManager.GetRegisteredUsersAsync(eventData.Entity));
            foreach (var user in registeredUsers)
            {
                var message = eventData.Entity.Title + " event's date is changed! New date is: " + eventData.Entity.Date;
                Logger.Debug(string.Format("TODO: Send email to {0} -> {1}", user.EmailAddress, message));
            }
        }
        //Triggered when an event is canceled. It's triggered in EventManager.Cancel method.
        public void HandleEvent(EventCancelledEvent eventData)
        {
            //TODO: Send email to all registered users!

            var registeredUsers = AsyncHelper.RunSync(() => _eventManager.GetRegisteredUsersAsync(eventData.Entity));
            foreach (var user in registeredUsers)
            {
                var message = eventData.Entity.Title + " event is canceled!";
                Logger.Debug(string.Format("TODO: Send email to {0} -> {1}", user.EmailAddress, message));
            }
        }
    }
}
