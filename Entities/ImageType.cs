using Newtonsoft.Json;

namespace UniversityProject.Entities
{
    public class ImageType
    {
        public Guid Id { get; set; }
        public int code { get; set; }
        public string   TypeName { get; set; }
        public List<Image> Images { get; set; }
    }
}
