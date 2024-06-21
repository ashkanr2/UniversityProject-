using Microsoft.EntityFrameworkCore;
using UniversityProject.Entities;
using UniversityProject.Infrastructures;
using UniversityProject.Interfaces;
using UniversityProject.Models;

namespace UniversityProject.Services
{

    public class ImageService : IImageService
    {
        private readonly UniversityDBContext _context;
        private readonly IWebHostEnvironment _env;
        public ImageService(UniversityDBContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env=env;
        }

        public async Task<IEnumerable<Image>> GetAllAsync()
        {
            return await _context.Images.ToListAsync();
        }

       public async Task<string> GetImageUrlByIdAsync(Guid imageId)
        {
            var image = await _context.Images.FindAsync(imageId);
            return image?.Url ?? "assets/images/cources/cource-1.jpg"; // Return a default image URL if not found
        }

        public async Task<ValidationResult> AddAsync(IFormFile file, Guid imageTypeId)
        {
            var validation = new ValidationResult();
            var image = new Image();

            try
            {
                if (file == null || file.Length == 0)
                {
                    validation.Error = "Invalid file.";
                    validation.Isvalid = false;
                    return validation;
                }

                var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                Directory.CreateDirectory(uploadsFolder); // Ensure the directory exists

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                // Generate the URL
                var url = $"/uploads/{uniqueFileName}";

                // Convert to Base64
                string base64File;
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    var fileBytes = memoryStream.ToArray();
                    base64File = Convert.ToBase64String(fileBytes);
                }

                image.Url = url;
                image.Base64File = base64File;
                image.IsDeleted = false;
                image.IsModified = true;
                image.ImageTypeId = imageTypeId;
                image.CreatedOn = DateTime.Now;

                _context.Images.Add(image);
                await _context.SaveChangesAsync();

                validation.Isvalid = true;
                validation.resultId = image.Id;
                return validation;
            }
            catch (Exception ex)
            {
                // Log the exception
                validation.Error = $"An error occurred while adding the image: {ex.Message}";
                validation.Isvalid = false;
                return validation;
            }

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
       public async Task<Guid> GetImageType(int imageTypeCode)
        {
            try
            {
                var ImageType = await _context.ImageType.FirstOrDefaultAsync(x => x.code == imageTypeCode);
                if (ImageType != null) { return ImageType.Id; }
                else return new Guid();

            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception($"An error occurred while find ImageType.", ex);
            }
        }
    }
}
