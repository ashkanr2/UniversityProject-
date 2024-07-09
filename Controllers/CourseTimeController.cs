using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using UniversityProject.Entities;
using UniversityProject.Interfaces;
using UniversityProject.Models;
using UniversityProject.Services;



namespace YourNamespace.Controllers
{
    public class CourseTimeController : Controller
    {
        private readonly ICourseTimeService _courseTimeService;

        public CourseTimeController(ICourseTimeService courseTimeService)
        {
            _courseTimeService = courseTimeService;
        }

        // GET: CourseTime/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CourseTime/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseTimeViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Convert selected days to a list
                var selectedDays = new List<DayOfWeek>();
                if (model.Sunday) selectedDays.Add(DayOfWeek.Sunday);
                if (model.Monday) selectedDays.Add(DayOfWeek.Monday);
                if (model.Tuesday) selectedDays.Add(DayOfWeek.Tuesday);
                if (model.Wednesday) selectedDays.Add(DayOfWeek.Wednesday);
                if (model.Thursday) selectedDays.Add(DayOfWeek.Thursday);
                if (model.Friday) selectedDays.Add(DayOfWeek.Friday);
                if (model.Saturday) selectedDays.Add(DayOfWeek.Saturday);

                // Create CourseTime entity
                var courseTime = new CourseTime
                {
                    Days = selectedDays,
                    Time = model.Time, // Time should already be properly parsed as TimeSpan
                    EndDate = model.EndDate,
                    IsDeleted = model.IsDeleted,
                    Name=model.Name
                    // Assign other properties as needed
                };

                await _courseTimeService.CreateAsync(courseTime);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
        // GET: CourseTime/Index
        public async Task<IActionResult> Index()
        {
            var courseTimes = await _courseTimeService.GetAllAsync();
            return View(courseTimes);
        }

        // GET: CourseTime/Details/{id}
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseTime = await _courseTimeService.GetByIdAsync(id.Value);
            if (courseTime == null)
            {
                return NotFound();
            }

            return View(courseTime);
        }

        // GET: CourseTime/Edit/{id}
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseTime = await _courseTimeService.GetByIdAsync(id.Value);
            if (courseTime == null)
            {
                return NotFound();
            }

            return View(courseTime);
        }

        // POST: CourseTime/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CourseTime courseTime)
        {
            if (id != courseTime.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _courseTimeService.UpdateAsync(courseTime);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CourseTimeExists(courseTime.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(courseTime);
        }

        // GET: CourseTime/Delete/{id}
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseTime = await _courseTimeService.GetByIdAsync(id.Value);
            if (courseTime == null)
            {
                return NotFound();
            }

            return View(courseTime);
        }

        // POST: CourseTime/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _courseTimeService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CourseTimeExists(Guid id)
        {
            var courseTime = await _courseTimeService.GetByIdAsync(id);
            return courseTime != null;
        }
    }
}