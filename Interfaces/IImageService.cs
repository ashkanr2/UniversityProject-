using UniversityProject.Entities;
using UniversityProject.Models;

namespace UniversityProject.Interfaces
{
    public interface IImageService
    {
        Task<IEnumerable<Image>> GetAllAsync();
        Task<string> GetImageUrlByIdAsync(Guid imageId);
        Task<ValidationResult> AddAsync(IFormFile File, Guid imageTypeId);
        Task UpdateAsync(Image image);
        Task DeleteAsync(Guid id);
        Task<Guid> GetImageType(int imageTypeCode);
    }
}
