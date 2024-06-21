using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace UniversityProject.Entities
{
    public class Course
    {
        [Key]
        public Guid Id { get; set; }

        public  string Name { get; set; }

        public  string Description { get; set; }
        public Guid TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public decimal Cost { get; set; }

        public bool  IsDeleted { get; set; }
        public bool  IsActive { get; set; }
        public  DateTime  CreatedOn { get; set; }
        public Image Image { get; set; }
        public Guid? ImageId { get; set; }
        public ICollection<UserCourse> UserCourses { get; set; }
        public ICollection<DayAndTime> Schedule { get; set; }
    }
}
