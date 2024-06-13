using Microsoft.EntityFrameworkCore;
using UniversityProject.Entities;
using UniversityProject.Infrastructures;
using UniversityProject.Interfaces;

namespace UniversityProject.Services
{

    public class ImageService : IImageService
    {
        private readonly UniversityDBContext _context;

        public ImageService(UniversityDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Image>> GetAllAsync()
        {
            return await _context.Images.ToListAsync();
        }

        public async Task<Image> GetByIdAsync(Guid id)
        {
            return await _context.Images.FindAsync(id);
        }

        public async Task AddAsync(Image image)
        {
            _context.Images.Add(image);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Image image)
        {
            _context.Entry(image).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var image = await _context.Images.FindAsync(id);
            if (image != null)
            {
                _context.Images.Remove(image);
                await _context.SaveChangesAsync();
            }
        }
    }
}
