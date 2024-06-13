using Microsoft.EntityFrameworkCore;
using UniversityProject.Entities;
using UniversityProject.Infrastructures;
using UniversityProject.Interfaces;

namespace UniversityProject.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly UniversityDBContext _context;

        public UserRoleService(UniversityDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserRole>> GetAllAsync()
        {
            return await _context.UserRoles.ToListAsync();
        }

        public async Task<UserRole> GetByIdAsync(int code)
        {
            return await _context.UserRoles.FindAsync(code);
        }

        public async Task AddAsync(UserRole userRole)
        {
            _context.UserRoles.Add(userRole);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserRole userRole)
        {
            _context.Entry(userRole).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int code)
        {
            var userRole = await _context.UserRoles.FindAsync(code);
            if (userRole != null)
            {
                _context.UserRoles.Remove(userRole);
                await _context.SaveChangesAsync();
            }
        }
    }
}
