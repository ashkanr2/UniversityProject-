using Microsoft.AspNetCore.Mvc;
using UniversityProject.Entities;
using UniversityProject.Interfaces;

namespace UniversityProject.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _lessonService;

        public CourseController(ICourseService lessonService)
        {
            _lessonService = lessonService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _lessonService.GetAllAsync());
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var lesson = await _lessonService.GetByIdAsync(id);
            if (lesson == null)
            {
                return NotFound();
            }
            return View(lesson);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,IsDeleted,IsActive,CreatedOn")] Course lesson)
        {
            if (ModelState.IsValid)
            {
                await _lessonService.AddAsync(lesson);
                return RedirectToAction(nameof(Index));
            }
            return View(lesson);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var lesson = await _lessonService.GetByIdAsync(id);
            if (lesson == null)
            {
                return NotFound();
            }
            return View(lesson);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Description,IsDeleted,IsActive,CreatedOn")] Course lesson)
        {
            if (id != lesson.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _lessonService.UpdateAsync(lesson);
                return RedirectToAction(nameof(Index));
            }
            return View(lesson);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var lesson = await _lessonService.GetByIdAsync(id);
            if (lesson == null)
            {
                return NotFound();
            }
            return View(lesson);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _lessonService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
