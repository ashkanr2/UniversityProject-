using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using UniversityProject.Entities;
using UniversityProject.Infrastructures;
using UniversityProject.Interfaces;
using YourNamespace.Controllers;

namespace UniversityProject.Services
{
    public class CourseTimeService : ICourseTimeService
    {
        private readonly UniversityDBContext _context;

        public CourseTimeService(UniversityDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CourseTime>> GetAllAsync()
        {
            return await _context.CourseTime.Where(ct => !ct.IsDeleted).ToListAsync();
        }

        public async Task<CourseTime> GetByIdAsync(Guid id)
        {
            return await _context.CourseTime.FindAsync(id);
        }

        public async Task CreateAsync(CourseTime courseTime)
        {
            try
            {
                courseTime.Id = Guid.NewGuid();
                _context.CourseTime.Add(courseTime);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new ApplicationException("An error occurred while creating the CourseTime", ex);
            }
        }

        public async Task UpdateAsync(CourseTime courseTime)
        {
            try
            {
                _context.CourseTime.Update(courseTime);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new ApplicationException("An error occurred while updating the CourseTime", ex);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var courseTime = await _context.CourseTime.FindAsync(id);
                if (courseTime != null)
                {
                    courseTime.IsDeleted = true;
                    _context.CourseTime.Update(courseTime);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new ApplicationException("An error occurred while deleting the CourseTime", ex);
            }
        }
    }
}

