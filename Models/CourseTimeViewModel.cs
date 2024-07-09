using System.ComponentModel.DataAnnotations;

namespace UniversityProject.Models
{
    public class CourseTimeViewModel
    {
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

        [Display(Name = "Time")]
        public TimeSpan Time { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Is Deleted")]
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
    }
}
