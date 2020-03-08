using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyProject.Persons.Dto
{
    public class CreatePersonInput
    {
        [Required]
        public string Name { get; set; }
        public string EmailAddress { get; set; }

    }
}
