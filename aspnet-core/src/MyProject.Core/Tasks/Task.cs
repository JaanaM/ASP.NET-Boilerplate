using Abp.Domain.Entities;
using MyProject.People;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyProject.Tasks
{
    class Task : Entity<long>
    { 
        [ForeignKey("AssignedPersonId")]
        public virtual Person AssignedPerson { get; set; }
        public virtual int? AssignedPersonId { get; set; }
        public virtual DateTime CreationTime { get; set; }

        public virtual TaskState State { get; set; }

        public Task()
        {
            CreationTime = DateTime.Now;
            State = TaskState.Active;
        }

    }
}
