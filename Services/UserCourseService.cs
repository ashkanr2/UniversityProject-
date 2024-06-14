using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
                var IsExist =await CourseIsExistForUser(userId , courseId);
                if (IsExist)
                {
                    return true; 
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

        public async Task<bool> CourseIsExistForUser(Guid userId, Guid courseId)
        {
            try
            {
                var result =  _context.UserCourses.Any(x=>x.CourseId == courseId && x.UserId == userId);
                
                return result;
            }
            catch (Exception)
            {
                  
                throw;
               
            }
        }

        public Task<IEnumerable<Course>> GetAllByUserId(Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Course>> GetAllCoursesByUserId(Guid userId)
        {
            try
            {
                var courses = await _context.UserCourses
                    .Where(x => x.UserId == userId)
                    .Select(x => x.course)
                    .ToListAsync();

                return courses;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching courses for user {userId}: {ex.Message}");
                throw;
            }
        }

    }

}
