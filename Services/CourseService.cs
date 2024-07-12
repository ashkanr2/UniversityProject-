using Microsoft.EntityFrameworkCore;
using UniversityProject.Entities;
using UniversityProject.Infrastructures;
using UniversityProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityProject.Models;

namespace UniversityProject.Services
{
    public class CourseService : ICourseService
    {
        private readonly UniversityDBContext _context;
        private readonly IUserCourseService _userCourseService;


        public CourseService(UniversityDBContext context, IUserCourseService userCourseService)
        {
            _context = context;
            _userCourseService= userCourseService;
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            try
            {
                return await _context.Courses.Include(x => x.Teacher).ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (example using a logging service)
                // _logger.LogError(ex, "An error occurred while getting all courses.");
                throw new Exception("An error occurred while getting all courses.", ex);
            }
        }

        public async Task<Course> GetByIdAsync(Guid id)
        {
            try
            {
                return await _context.Courses.Include(x => x.Teacher).FirstOrDefaultAsync(x => x.Id ==  id);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception($"An error occurred while getting the course with ID {id}.", ex);
            }
        }

        public async Task<string> AddAsync(Course course)
        {
            try
            {
                course.CreatedOn=DateTime.Now;
                course.IsDeleted=false;
                _context.Courses.Add(course);
                await _context.SaveChangesAsync();
                return "course Added successfully";
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("An error occurred while adding the course.", ex);
            }
        }

        public async Task<string> UpdateAsync(Course course)
        {

            try
            {
                var courseModel = await _context.Courses.FirstOrDefaultAsync(x => x.Id == course.Id);
                if (courseModel == null)
                {
                    return "Error Course Not Found";
                }
                courseModel.Cost = course.Cost;
                courseModel.ImageId= course.ImageId;
                courseModel.Name = course.Name;
                courseModel.Description = course.Description;
                courseModel.TeacherId = course.TeacherId;
                courseModel.IsActive = course.IsActive;
                courseModel.IsDeleted= course.IsDeleted;
                courseModel.StartTime= course.StartTime;
                courseModel.EndDate= course.EndDate;
                courseModel.StartDate= course.StartDate;
                courseModel.EndTime= course.EndTime;
                await _context.SaveChangesAsync();
                return "course updated successfully";
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception($"An error occurred while updating the course with ID {course.Id}.", ex);
            }
        }

        public async Task<string> DeleteAsync(Guid id)
        {
            try
            {
                var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == id);
                if (course == null)
                {
                    return "Course Not Found";
                }
                course.IsDeleted=true;
                await _context.SaveChangesAsync();
                return "course Deleted successfully";
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception($"An error occurred while deleting the course with ID {id}.", ex);
            }
        }

        public async Task<IEnumerable<Course>> GetAllByTeacherId(Guid id)
        {
            try
            {
                return await _context.Courses.Where(x => x.TeacherId == id).ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception($"An error occurred while getting courses for the teacher with ID {id}.", ex);
            }
        }
        public async Task<List<Course>> SearchCourses(string query)
        {
            // Search for courses where course name contains the query
            var coursesQuery = _context.Courses
               .Include(x => x.Teacher)
                .Where(c => c.Name.Contains(query) || (c.Teacher != null && c.Teacher.Name.Contains(query)))
                .ToListAsync();

            return await coursesQuery;
        }

        public async Task<List<CourseListVM>> GetAllUserCourses(Guid userId)
        {
            var courselistvm = new List<CourseListVM>();
            var courses = await _userCourseService.GetAllCoursesByUserId(userId);

            foreach (var course in courses)
            {
                int studentNumbr = await _userCourseService.CalculateStudentCount(course.Id);
                var courseVM = new CourseListVM
                {
                    Id = course.Id,
                    StartDate= course.StartDate,
                    EndDate=course.EndDate,
                    StartTime=course.StartTime, 
                    EndTime=course.EndTime,
                    Name = course.Name,
                    Description = course.Description,
                    TeacherName = course.Teacher.Name,
                    Cost = course.Cost,
                    IsDeleted = course.IsDeleted,
                    IsActive = course.IsActive,
                    CreatedOn = course.CreatedOn,
                    Image = course.Image,
                    ImageId = course.ImageId,
                    StudentNumber = studentNumbr
                };

                courselistvm.Add(courseVM);
            }

            return courselistvm;
        }
        public string CanUserAddCourse(Guid userId, Guid courseId)
        {
            var newCourse = _context.Courses.Find(courseId);

            if (newCourse == null)
            {
                return "Error: Course does not exist.";
            }

            var userCourses = _context.UserCourses
                                      .Where(uc => uc.UserId == userId)
                                      .Select(uc => uc.course)
                                      .ToList();

            if (userCourses.Any(c => c.Id == courseId))
            {
                return "Error: You are already enrolled in this course.";
            }
            foreach (var existingCourse in userCourses)
            {
                if (existingCourse.Days.Intersect(newCourse.Days).Any())
                {
                    var newCourseStartTime = newCourse.StartDate.Add(newCourse.StartTime);
                    var newCourseEndTime = newCourse.EndDate.Add(newCourse.StartTime);

                    var existingCourseStartTime = existingCourse.StartDate.Add(existingCourse.StartTime);
                    var existingCourseEndTime = existingCourse.EndDate.Add(existingCourse.StartTime);

                    if ((newCourseStartTime < existingCourseEndTime && newCourseEndTime > existingCourseStartTime) ||
                        (existingCourseStartTime < newCourseEndTime && existingCourseEndTime > newCourseStartTime))
                    {

                        return "Error: = Course  times conflict with existing course  =>  "+ (existingCourse.Name).ToString() ;
                    }
                }
            }

            return "Success: User can add this course.";
        }

        

    }
}
