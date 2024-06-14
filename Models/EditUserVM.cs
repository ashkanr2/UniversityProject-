namespace UniversityProject.Models
{
    public class EditUserVM
    {
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime Birthdate { get; set; }
        public bool IssystemAdmin { get; set; }
        public IFormFile ProfileImage { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsModified { get; set; }
    }

}
