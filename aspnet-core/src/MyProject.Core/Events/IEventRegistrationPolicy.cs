using System.Threading.Tasks;
using Abp.Domain.Services;
using MyProject.Authorization.Users;

namespace MyProject.Events
{
    public interface IEventRegistrationPolicy
    {
        /// <summary>
        /// Checks if given user can register to <see cref="@event"/> and throws exception if can not.
        /// </summary>
        Task CheckRegistrationAttemptAsync(Event @event, User user);
    }
}