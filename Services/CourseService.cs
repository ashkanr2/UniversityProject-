using Microsoft.EntityFrameworkCore;
using UniversityProject.Entities;
using UniversityProject.Infrastructures;
using UniversityProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityProject.Services
{
    public class CourseService : ICourseService
    {
        private readonly UniversityDBContext _context;

        public CourseService(UniversityDBContext context)
        {
            _context = context;
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
                return await _context.Courses.FindAsync(id);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception($"An error occurred while getting the course with ID {id}.", ex);
            }
        }

        public async Task AddAsync(Course lesson)
        {
            try
            {
                _context.Courses.Add(lesson);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("An error occurred while adding the course.", ex);
            }
        }

        public async Task UpdateAsync(Course lesson)
        {
            try
            {
                _context.Entry(lesson).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception($"An error occurred while updating the course with ID {lesson.Id}.", ex);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var lesson = await _context.Courses.FindAsync(id);
                if (lesson != null)
                {
                    _context.Courses.Remove(lesson);
                    await _context.SaveChangesAsync();
                }
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
    }
}
