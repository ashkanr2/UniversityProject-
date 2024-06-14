using Microsoft.AspNetCore.Identity;
using UniversityProject.Entities;
using UniversityProject.Infrastructures;
using UniversityProject.Interfaces;

namespace UniversityProject.Services
{
    public class UserCourseService : IUserCourseService
    {
        private readonly UserManager<User> _userManager;
        private readonly UniversityDBContext _context;
        public UserCourseService(UserManager<User> userManager , UniversityDBContext context )
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<bool> AddCourseForUser(Guid userId, Guid courseId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId.ToString());
                if (user == null)
                {
                    return false;
                }
                // Check if the user is already enrolled in the course
                if (user.UserCourses.Any(uc => uc.CourseId == courseId))
                {
                    return true; // User is already enrolled
                }

                var userCourse = new UserCourse
                {
                    UserId = userId,
                    CourseId = courseId
                };
                 _context.UserCourses.Add(userCourse);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error enrolling user in course: {ex.Message}");
                throw; // Rethrow the exception for the controller to handle
            }
        }
    }

}
