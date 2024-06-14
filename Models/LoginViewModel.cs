using System.ComponentModel.DataAnnotations;

namespace UniversityProject.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "ایمیل")]
        [EmailAddress]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
