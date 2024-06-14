using Microsoft.EntityFrameworkCore;
using UniversityProject.Entities;
using UniversityProject.Infrastructures;
using UniversityProject.Interfaces;

namespace UniversityProject.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly UniversityDBContext _context;

        public TeacherService(UniversityDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Teacher>> GetAllAsync()
        {
            try
            {
                return await _context.Teachers.ToListAsync();
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"An error occurred while getting all teachers: {ex.Message}");
                return null;
            }
        }

        public async Task<Teacher> GetByIdAsync(Guid id)
        {
            try
            {
                return await _context.Teachers.FirstOrDefaultAsync(t => t.Id == id);
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"An error occurred while getting teacher by id: {ex.Message}");
                return null;
            }
        }

        public async Task AddAsync(Teacher teacher)
        {
            int teacherCode = await TeacherCodeGenerator();
            try
            {
                teacher.TeacherCode= teacherCode;
                _context.Teachers.Add(teacher);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"An error occurred while adding teacher: {ex.Message}");
            }
        }

        public async Task UpdateAsync(Teacher teacher)
        {
            try
            {
                _context.Teachers.Update(teacher);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"An error occurred while updating teacher: {ex.Message}");
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var teacher = await _context.Teachers.FirstOrDefaultAsync(t => t.Id == id);
                if (teacher != null)
                {
                    _context.Teachers.Remove(teacher);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"An error occurred while deleting teacher: {ex.Message}");
            }
        }

        public async Task<Teacher> GetByIdUser(Guid id)
        {
            try
            {
                var teacher =  await _context.Teachers.FirstOrDefaultAsync(t => t.UserId==id);
                return teacher;
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"An error occurred while getting teacher by USer Id: {ex.Message}");
                return null;
            }
        }
        public async Task<int> TeacherCodeGenerator()
        {
           int result =  await _context.Teachers.MaxAsync(x => x.TeacherCode);
            if (result == 0 || result ==  null)
            {
                return 1;
            }
            return result;
        }
    }
}
