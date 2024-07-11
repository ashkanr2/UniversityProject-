using UniversityProject.Entities;
using UniversityProject.Models;

namespace UniversityProject.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetAllAsync();
        Task<Course> GetByIdAsync(Guid id);
        Task<IEnumerable<Course>> GetAllByTeacherId(Guid id);
        Task<List<Course>> SearchCourses(string query);
        Task<string> AddAsync(Course lesson);
        Task<string> UpdateAsync(Course lesson);
        Task<string> DeleteAsync(Guid id);
        Task<List<CourseListVM>> GetAllUserCourses(Guid userId);
        string CanUserAddCourse(Guid userId, Guid courseId);



    }
}
