using System;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using MyProject.Events.Dtos;

namespace MyProject.Events
{
    /// <summary>
    /// Each application service (for EventAppService) is implemented here 
    /// Each method uses DTO created on Event.Dtos
    /// </summary>
    internal interface IEventAppService
    {
        Task<ListResultDto<EventListDto>> GetListAsync(GetEventListInput input);

        Task<EventDetailOutput> GetDetailAsync(EntityDto<Guid> input);

        Task CreateAsync(CreateEventInput input);

        Task CancelAsync(EntityDto<Guid> input);

        Task<EventRegisterOutput> RegisterAsync(EntityDto<Guid> input);

        Task CancelRegistrationAsync(EntityDto<Guid> input);
    }
}