using UniversityProject.Entities;
using UniversityProject.Models;

namespace UniversityProject.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(Guid id);
        Task AddAsync(User user);
        Task UpdateAsync(EditUserVM model);
        Task DeleteAsync(Guid id);

    }


}
