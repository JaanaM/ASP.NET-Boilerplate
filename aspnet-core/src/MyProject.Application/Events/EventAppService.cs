using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using Abp.UI;
using System.Threading.Tasks;
using MyProject.Events.Dtos;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MyProject.Authorization.Users;

namespace MyProject.Events
{
    /// <summary>
    /// Uses AppServiceBase and interface IEventAppService 
    /// IEventAppService has all the implemented Methods
    /// Application services use domain layer to implement use cases of the application (generally used by presentation layer)
    /// EventAppService performs application logic for events.
    /// </summary>
    [AbpAuthorize]
    public class EventAppService : MyProjectAppServiceBase, IEventAppService
    {
        private readonly IEventManager _eventManager;
        private readonly IRepository<Event, Guid> _eventRepository;

        public EventAppService(IEventManager eventManager, IRepository<Event, Guid> eventRepository)
        {
            _eventManager = eventManager;
            _eventRepository = eventRepository;
        }
        // Returns event list entity (which are not canceled)
        //
        public async Task<ListResultDto<EventListDto>> GetListAsync(GetEventListInput input)
        {
            var events = await _eventRepository
               .GetAll()
               .Include(e => e.Registrations)
               .WhereIf(!input.IncludeCanceledEvents, e => !e.IsCancelled)
               .OrderByDescending(e => e.CreationTime)
               .Take(64)
               .ToListAsync();

            return new ListResultDto<EventListDto>(events.MapTo<List<EventListDto>>());
        }
        // Get event details
        public async Task <EventDetailOutput> GetDetailAsync(EntityDto<Guid> input)
        {
            var @event = await _eventRepository
           .GetAll()
           .Include(e => e.Registrations)
           .ThenInclude(r => r.User)
           .Where(e => e.Id == input.Id)
           .FirstOrDefaultAsync();

            if (@event == null)
            {
                throw new UserFriendlyException("Could not found the event, maybe it's deleted.");
            }

            return @event.MapTo<EventDetailOutput>();
        }
        // creates event according given inputs
        public async Task CreateAsync(CreateEventInput input)
        {
            var @event = Event.Create(AbpSession.GetTenantId(), input.Title, input.Date, input.Description, input.MaxRegistrationCount);
            await _eventManager.CreateAsync(@event);
        }
        // cancels event task
        public async Task CancelAsync(EntityDto<Guid> input)
        {
            var @event = await _eventManager.GetAsync(input.Id);
            _eventManager.Cancel(@event);
        }
        //register to a event
        public async Task<EventRegisterOutput> RegisterAsync(EntityDto<Guid> input)
        {
            var registration = await RegisterAndSaveAsync(
                await _eventManager.GetAsync(input.Id),
                await GetCurrentUserAsync()
                );

            return new EventRegisterOutput
            {
                RegistrationId = registration.Id
            };
        }
        //cancel registration
        //application service does not implement domain logic itself. It just uses entities and domain services (EventManager) to perform the use cases.
        public async Task CancelRegistrationAsync(EntityDto<Guid> input)
        {
            await _eventManager.CancelRegistrationAsync(
                await _eventManager.GetAsync(input.Id),
                await GetCurrentUserAsync()
                );
        }
        private async Task<EventRegistration> RegisterAndSaveAsync(Event @event, User user)
        {
            var registration = await _eventManager.RegisterAsync(@event, user);
            await CurrentUnitOfWork.SaveChangesAsync();
            return registration;
        }


    }
}
