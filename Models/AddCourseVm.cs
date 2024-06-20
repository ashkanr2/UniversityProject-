using System.ComponentModel.DataAnnotations;
using UniversityProject.Entities;

namespace UniversityProject.Models
{
    public class AddCourseVm
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public Guid SelectedTeacherId { get; set; }
        public decimal Cost { get; set; }

        public bool IsActive { get; set; }
        public List<Teacher> Teachers { get; set; }
        public   IFormFile Image { get; set; }
        
    }
}
