using UniversityProject.Entities;

namespace UniversityProject.Interfaces
{
    public interface IImageService
    {
        Task<IEnumerable<Image>> GetAllAsync();
        Task<Image> GetByIdAsync(Guid id);
        Task AddAsync(Image image);
        Task UpdateAsync(Image image);
        Task DeleteAsync(Guid id);
    }
}
