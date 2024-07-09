using UniversityProject.Entities;
using UniversityProject.Models;

namespace UniversityProject.Interfaces
{
    public interface ICourseTimeService
    {
        Task<IEnumerable<CourseTimeListVm>> GetAllAsync();
        Task<CourseTime> GetByIdAsync(Guid id);
        Task CreateAsync(CourseTime courseTime);
        Task UpdateAsync(CourseTime courseTime);
        Task DeleteAsync(Guid id);
    }
}
