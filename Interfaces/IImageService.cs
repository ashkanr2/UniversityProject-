using UniversityProject.Entities;
using UniversityProject.Models;

namespace UniversityProject.Interfaces
{
    public interface IImageService
    {
        Task<IEnumerable<Image>> GetAllAsync();
        Task<Image> GetByIdAsync(Guid id);
        Task<ValidationResult> AddAsync(IFormFile File, Guid imageTypeId);
        Task UpdateAsync(Image image);
        Task DeleteAsync(Guid id);
        Task<Guid> GetImageType(int imageTypeCode);
    }
}
