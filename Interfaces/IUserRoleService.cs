using UniversityProject.Entities;

namespace UniversityProject.Interfaces
{
    public interface IUserRoleService
    {
        Task<IEnumerable<UserRole>> GetAllAsync();
        Task<UserRole> GetByIdAsync(int code);
        Task AddAsync(UserRole userRole);
        Task UpdateAsync(UserRole userRole);
        Task DeleteAsync(int code);
    }
}
