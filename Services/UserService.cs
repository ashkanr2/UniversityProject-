using Microsoft.EntityFrameworkCore;
using UniversityProject.Entities;
using UniversityProject.Infrastructures;
using UniversityProject.Interfaces;
using UniversityProject.Models;

namespace UniversityProject.Services
{
    public class UserService : IUserService
    {
        private readonly UniversityDBContext _context;

        public UserService(UniversityDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users
                .Where(u => !u.IsDeleted)
                .ToListAsync();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
        }

        public async Task AddAsync(User user)
        {
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(EditUserVM model)
        {
            var user = await _context.Users.FindAsync(model.Id);
            if (user != null)
            {
                user.Firstname = model.Firstname;
                user.Lastname = model.Lastname;
                user.Birthdate = model.Birthdate;
                user.IssystemAdmin = model.IssystemAdmin;
                user.IsDeleted = model.IsDeleted;
                user.IsModified = model.IsModified;

                // Handle ProfileImage upload if necessary
                // Example: user.ImageId = ...;

                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                user.IsDeleted = true;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
        }

    }
}
