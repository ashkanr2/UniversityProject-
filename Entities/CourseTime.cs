using System.ComponentModel.DataAnnotations;

namespace UniversityProject.Entities
{
    public class CourseTime
    {
        [Key]
        public Guid Id { get; set; }

        public string   Name { get; set; }

        public List<DayOfWeek> Days { get; set; } 

        public TimeSpan Time { get; set; }  

        public DateTime StartDate { get; set; }  

        public DateTime EndDate { get; set; }  

        public bool IsDeleted { get; set; }

         public ICollection<Course> Courses { get; set; }
    }
}
