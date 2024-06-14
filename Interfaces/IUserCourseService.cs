namespace UniversityProject.Interfaces
{
    public interface IUserCourseService
    {
        Task<bool> AddCourseForUser(Guid userId, Guid courseId);
    }

}
