using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Persons.Dto
{
    class PersonDto : EntityDto
    {
        public virtual string Name { get; set; }
    }
}
