using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using MyProject.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyProject.Events
{
    class EventRegistration : CreationAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [ForeignKey("EventId")]
        public virtual Event Event { get; protected set; }
        public virtual Guid EventId { get; protected set; }

        [ForeignKey("UserId")]
        public virtual User User { get; protected set; }
        public virtual long UserId { get; protected set; }

        protected EventRegistration()
        {

        }


    }
}
