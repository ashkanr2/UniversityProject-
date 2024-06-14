using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace UniversityProject.Entities
{
        public class User:IdentityUser<Guid>
        {

            [Key]
            //public Guid Id { get; set; }
            public string Firstname { get; set; }
            public string Lastname { get; set; }
            public string HelpPassword { get; set; }  

            public DateTime Birthdate { get; set; }
            public DateTime CreatedOn { get; set; }
            public bool IssystemAdmin { get; set; }
            public Guid? ImageId { get; set; }
            public Image? ProfileImage { get; set; }
            public bool IsDeleted { get; set; }
            public bool IsModified { get; set; }
            public  List<UserRole> roles { get; set; }
            //public List<Course>? courses { get; set; }
        }
}
