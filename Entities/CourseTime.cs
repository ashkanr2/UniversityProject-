using System.ComponentModel.DataAnnotations;

namespace UniversityProject.Entities
{
    public class CourseTime
    {
        [Key]
        public Guid Id { get; set; }
        public DayOfWeek Day { get; set; }
        public TimeSpan Time { get; set; }
        public bool IsDeleted  { get; set; }
        //public ICollection<Course> Courses { get; set; }
    }
}
