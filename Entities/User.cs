using System.ComponentModel.DataAnnotations;

namespace UniversityProject.Entities
{
    public class User
    {

        [Key]
        public Guid Id { get; set; }
        public string Firstnam { get; set; }
        public string Lastnam { get; set; }

        public DateTime Birthdate { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IssystemAdmin { get; set; }
        public Guid? ImageId { get; set; }
        public Image? ProfileImage { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsModified { get; set; }
        public  List<UserRole> roles { get; set; }
    }
}
