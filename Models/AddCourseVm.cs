using System.ComponentModel.DataAnnotations;
using UniversityProject.Entities;

namespace UniversityProject.Models
{
    public class AddCourseVm
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please select a teacher")]
        public Guid SelectedTeacherId { get; set; }

        [Required(ErrorMessage = "Cost is required")]
        public decimal Cost { get; set; }

        public bool IsActive { get; set; }
        public List<Teacher>? Teachers { get; set; }
        public IFormFile? Image { get; set; }

        [Display(Name = "Sunday")]
        public bool Sunday { get; set; }

        [Display(Name = "Monday")]
        public bool Monday { get; set; }

        [Display(Name = "Tuesday")]
        public bool Tuesday { get; set; }

        [Display(Name = "Wednesday")]
        public bool Wednesday { get; set; }

        [Display(Name = "Thursday")]
        public bool Thursday { get; set; }

        [Display(Name = "Friday")]
        public bool Friday { get; set; }

        [Display(Name = "Saturday")]
        public bool Saturday { get; set; }

        [Display(Name = "StartTime")]
        [Required(ErrorMessage = "Start time is required")]
        public TimeSpan StartTime { get; set; }

        [Display(Name = "EndTime")]
        [Required(ErrorMessage = "End time is required")]
        public TimeSpan EndTime { get; set; }

        [Display(Name = "Start Date")]
        [Required(ErrorMessage = "Start date is required")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [Required(ErrorMessage = "End date is required")]
        public DateTime EndDate { get; set; }
    }
}
