using System.ComponentModel.DataAnnotations;

namespace UniversityProject.Entities
{
    public class UserRole
    {
        [Key]
        public  int Code { get; set; }

        public string Name { get; set; }

        public bool Isdeleted { get; set; }
        public bool Description { get; set; }
    }
}
