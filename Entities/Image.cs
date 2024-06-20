using System.ComponentModel.DataAnnotations;

namespace UniversityProject.Entities
{
    public class Image
    {
        [Key]
        public Guid Id { get; set; }
        public string Url { get; set; }
        public string Base64File { get; set; }
        public bool IsDeleted { get; set; }

        public bool IsModified { get; set; }
        public DateTime CreatedOn { get; set; }

        public Guid ImageTypeId { get; set; }
        public ImageType ImageType { get; set; }
        public List<Course> Courses { get; set; }
    }
}
