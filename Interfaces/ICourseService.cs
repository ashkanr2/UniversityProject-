using UniversityProject.Entities;

namespace UniversityProject.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetAllAsync();
        Task<Course> GetByIdAsync(Guid id);
        Task<IEnumerable<Course>> GetAllByTeacherId(Guid id);
        Task<List<Course>> SearchCourses(string query);
        Task AddAsync(Course lesson);
        Task UpdateAsync(Course lesson);
        Task DeleteAsync(Guid id);
    }
}
