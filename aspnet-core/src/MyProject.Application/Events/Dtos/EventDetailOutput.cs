using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyProject.Events.Dtos
{
    /// <summary>
    /// Automapped to Event  using  FullAuditedEntityDto to have creationTime, Creator, isDeleted, LastModified etc.
    [AutoMapFrom(typeof(Event))]
    public class EventDetailOutput: FullAuditedEntityDto<Guid>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public bool IsCancelled { get; set; }

        public virtual int MaxRegistrationCount { get; protected set; }

        public int RegistrationsCount { get; set; }

        public ICollection<EventRegistrationDto> Registrations { get; set; }

    }
}