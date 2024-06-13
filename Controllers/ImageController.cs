using Microsoft.AspNetCore.Mvc;
using UniversityProject.Entities;
using UniversityProject.Interfaces;

namespace UniversityProject.Controllers
{
    public class ImageController : Controller
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _imageService.GetAllAsync());
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var image = await _imageService.GetByIdAsync(id);
            if (image == null)
            {
                return NotFound();
            }
            return View(image);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Url,Base64File,IsDeleted,IsModified,CreatedOn")] Image image)
        {
            if (ModelState.IsValid)
            {
                await _imageService.AddAsync(image);
                return RedirectToAction(nameof(Index));
            }
            return View(image);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var image = await _imageService.GetByIdAsync(id);
            if (image == null)
            {
                return NotFound();
            }
            return View(image);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Url,Base64File,IsDeleted,IsModified,CreatedOn")] Image image)
        {
            if (id != image.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _imageService.UpdateAsync(image);
                return RedirectToAction(nameof(Index));
            }
            return View(image);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var image = await _imageService.GetByIdAsync(id);
            if (image == null)
            {
                return NotFound();
            }
            return View(image);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _imageService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
