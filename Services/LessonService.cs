using Microsoft.EntityFrameworkCore;
using UniversityProject.Entities;
using UniversityProject.Infrastructures;
using UniversityProject.Interfaces;

namespace UniversityProject.Services
{
    public class LessonService : ILessonService
    {
        private readonly UniversityDBContext _context;

        public LessonService(UniversityDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Lesson>> GetAllAsync()
        {
            return await _context.Lessons.ToListAsync();
        }

        public async Task<Lesson> GetByIdAsync(Guid id)
        {
            return await _context.Lessons.FindAsync(id);
        }

        public async Task AddAsync(Lesson lesson)
        {
            _context.Lessons.Add(lesson);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Lesson lesson)
        {
            _context.Entry(lesson).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var lesson = await _context.Lessons.FindAsync(id);
            if (lesson != null)
            {
                _context.Lessons.Remove(lesson);
                await _context.SaveChangesAsync();
            }
        }
    }
}
