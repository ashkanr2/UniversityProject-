using UniversityProject.Entities;

namespace UniversityProject.Interfaces
{
    public interface ILessonService
    {
        Task<IEnumerable<Lesson>> GetAllAsync();
        Task<Lesson> GetByIdAsync(Guid id);
        Task AddAsync(Lesson lesson);
        Task UpdateAsync(Lesson lesson);
        Task DeleteAsync(Guid id);
    }
}
