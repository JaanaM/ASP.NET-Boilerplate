using System;
using System.ComponentModel.DataAnnotations;

namespace MyProject.Events.Dtos
{
    /// <summary>
    /// Contains imput elements for the event creation Title, Description, Date, MaxRegistrationCount
    /// 
    /// </summary>
    public class CreateEventInput
    {
        [Required]
        // Uses validation which we created on Core.Events
        [StringLength(Event.MaxTitleLength)]
        public string Title { get; set; }
        // Uses validation which we created on Core.Events
        [StringLength(Event.MaxDescriptionLength)]
        public string Description { get; set; }

        public DateTime Date { get; set; }
        
        [Range(0, int.MaxValue)]
        public int MaxRegistrationCount { get; set; }
    }
}