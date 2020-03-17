using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.UI;
using MyProject.Authorization.Users;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Repositories;
using System.Threading.Tasks;

namespace MyProject.Events
{
    [Table("AppEventRegistrations")]
    public class EventRegistration : CreationAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [ForeignKey("EventId")]
        public virtual Event Event { get; protected set; }
        public virtual Guid EventId { get; protected set; }

        [ForeignKey("UserId")]
        public virtual User User { get; protected set; }
        public virtual long UserId { get; protected set; }

        /// <summary>
        /// We don't make constructor public and forcing to create registrations using <see cref="CreateAsync"/> method.
        /// But constructor can not be private since it's used by EntityFramework.
        /// </summary>
        protected EventRegistration()
        {

        }

        //he only way of creating a new EventRegistration is this CreateAsync method.
        //It gets an event, user and a registration policy. It checks if given user can register to the event using 
        //registrationPolicy.CheckRegistrationAttemptAsync method
        public static async Task<EventRegistration> CreateAsync(Event @event, User user, IEventRegistrationPolicy registrationPolicy)
        {
            await registrationPolicy.CheckRegistrationAttemptAsync(@event, user);

            return new EventRegistration
            {
                TenantId = @event.TenantId,
                EventId = @event.Id,
                Event = @event,
                UserId = @user.Id,
                User = user
            };
        }

        public async Task CancelAsync(IRepository<EventRegistration> repository)
        {
            if (repository == null) { throw new ArgumentNullException("repository"); }

            if (Event.IsInPast())
            {
                throw new UserFriendlyException("Can not cancel event which is in the past!");
            }

            if (Event.IsAllowedCancellationTimeEnded())
            {
                throw new UserFriendlyException("It's too late to cancel your registration!");
            }

            await repository.DeleteAsync(this);
        }
    }
}
