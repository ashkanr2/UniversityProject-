using UniversityProject.Entities;

namespace UniversityProject.Interfaces
{
    public interface IUserCourseService
    {
        Task<bool> AddCourseForUser(Guid userId, Guid courseId);
        Task<IEnumerable<Course>> GetAllByUserId(Guid userId);
        Task<bool>CourseIsExistForUser(Guid userId ,Guid courseId);
    }

}
