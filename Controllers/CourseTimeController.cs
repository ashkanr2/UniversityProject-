using Microsoft.AspNetCore.Mvc;
using UniversityProject.Entities;
using UniversityProject.Interfaces;


namespace YourNamespace.Controllers
{
    public class CourseTimeController : Controller
    {
        private readonly ICourseTimeService _courseTimeService;

        public CourseTimeController(ICourseTimeService courseTimeService)
        {
            _courseTimeService = courseTimeService;
        }

        public async Task<IActionResult> Index()
        {
            var courseTimes = await _courseTimeService.GetAllAsync();
            return View(courseTimes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseTime courseTime)
        {
            if (ModelState.IsValid)
            {
                await _courseTimeService.CreateAsync(courseTime);
                return RedirectToAction(nameof(Index));
            }
            return View(courseTime);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var courseTime = await _courseTimeService.GetByIdAsync(id);
            if (courseTime == null)
            {
                return NotFound();
            }
            return View(courseTime);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CourseTime courseTime)
        {
            if (id != courseTime.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _courseTimeService.UpdateAsync(courseTime);
                }
                catch (ApplicationException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(courseTime);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(courseTime);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var courseTime = await _courseTimeService.GetByIdAsync(id);
            if (courseTime == null)
            {
                return NotFound();
            }
            return View(courseTime);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                await _courseTimeService.DeleteAsync(id);
            }
            catch (ApplicationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(await _courseTimeService.GetByIdAsync(id));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
