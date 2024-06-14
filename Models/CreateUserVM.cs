namespace UniversityProject.Models
{
    public class CreateUserVM
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime Birthdate { get; set; }
        public bool IssystemAdmin { get; set; }
        public IFormFile ProfileImage { get; set; }
    }
}
