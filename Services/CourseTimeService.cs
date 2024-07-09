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
            return await _context.CourseTime.ToListAsync();
        }

        public async Task<CourseTime> GetByIdAsync(Guid id)
        {
            return await _context.CourseTime.FindAsync(id);
        }

        public async Task CreateAsync(CourseTime courseTime)
        {
            try
            {
                _context.CourseTime.Add(courseTime);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                Console.WriteLine();
                throw;
            }
          
        }

        public async Task UpdateAsync(CourseTime courseTime)
        {
            _context.Entry(courseTime).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var courseTime = await _context.CourseTime.FindAsync(id);
            if (courseTime != null)
            {
                _context.CourseTime.Remove(courseTime);
                await _context.SaveChangesAsync();
            }
        }
    }
}