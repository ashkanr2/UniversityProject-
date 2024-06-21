using System.ComponentModel.DataAnnotations;
using UniversityProject.Entities;

namespace UniversityProject.Models
{
    public class CourseListVM
    {
         [Key]
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string TeacherName { get; set; }
            public decimal Cost { get; set; }
            public bool IsDeleted { get; set; }
            public bool IsActive { get; set; }
            public DateTime CreatedOn { get; set; }
            public Image Image { get; set; }
            public Guid? ImageId { get; set; }
            public int StudentNumber { get; set; }

    }
}
