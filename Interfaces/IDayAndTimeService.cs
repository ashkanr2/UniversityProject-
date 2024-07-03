using Microsoft.VisualBasic;
using UniversityProject.Entities;
using UniversityProject.Models;

namespace UniversityProject.Interfaces
{
    public interface IDayAndTimeService
    {
        Task<IEnumerable<DateAndTime>> GetAllAsync();
        Task<DateAndTime> GetByIdAsync(Guid id);
        Task<string> AddAsync(DateAndTime dateAndTime);
        Task<string> UpdateAsync(DateAndTime dateAndTime);
        Task<string> DeleteAsync(Guid id);
    }
}
