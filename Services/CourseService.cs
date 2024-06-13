using Microsoft.EntityFrameworkCore;
using UniversityProject.Entities;
using UniversityProject.Infrastructures;
using UniversityProject.Interfaces;

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
            return await _context.Courses.Include(x=>x.Teacher).ToListAsync();
        }

        public async Task<Course> GetByIdAsync(Guid id)
        {
            return await _context.Courses.FindAsync(id);
        }

        public async Task AddAsync(Course lesson)
        {
            _context.Courses.Add(lesson);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Course lesson)
        {
            _context.Entry(lesson).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var lesson = await _context.Courses.FindAsync(id);
            if (lesson != null)
            {
                _context.Courses.Remove(lesson);
                await _context.SaveChangesAsync();
            }
        }
    }
}
