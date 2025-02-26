﻿using UniversityProject.Entities;

namespace UniversityProject.Interfaces
{
    public interface ITeacherService
    {

        Task<IEnumerable<Teacher>> GetAllAsync();
        Task<Teacher> GetByIdAsync(Guid id);
        Task AddAsync(Teacher teacher);
        Task<List<Teacher>> SearchTeachers(string query);
        Task UpdateAsync(Teacher teacher);
        Task DeleteAsync(Guid id);
        Task<Teacher> GetByIdUser(Guid id);
    }
}
