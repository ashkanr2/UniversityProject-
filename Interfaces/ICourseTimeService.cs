using UniversityProject.Entities;

namespace UniversityProject.Interfaces
{
    public interface ICourseTimeService
    {
        Task<IEnumerable<CourseTime>> GetAllAsync();
        Task<CourseTime> GetByIdAsync(Guid id);
        Task CreateAsync(CourseTime courseTime);
        Task UpdateAsync(CourseTime courseTime);
        Task DeleteAsync(Guid id);
    }
}
