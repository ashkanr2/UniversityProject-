using System.ComponentModel.DataAnnotations;
using UniversityProject.Entities;

namespace UniversityProject.Models
{
    public class CourseTimeListVm
    {
        [Key]
        public Guid Id { get; set; }

        public  string Days{ get; set; }

        public TimeSpan Time { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsDeleted { get; set; }
        public string Name { get; set; }

    }
}
