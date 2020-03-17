using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.People
{
    class Person : Entity
    {
        public virtual string Name { get; set; }
    }
}
