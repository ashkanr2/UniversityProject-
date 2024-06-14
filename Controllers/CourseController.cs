using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UniversityProject.Entities;
using UniversityProject.Interfaces;
using UniversityProject.Models;
using UniversityProject.Services;

namespace UniversityProject.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly ITeacherService _teacherservice;
        private readonly SignInManager<User> _signInManager;
        public CourseController(ICourseService lessonService, ITeacherService teacherservic, SignInManager<User> signInManager)
        {
            _courseService = lessonService;
            _teacherservice=teacherservic;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _courseService.GetAllAsync());
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var lesson = await _courseService.GetByIdAsync(id);
            if (lesson == null)
            {
                return NotFound();
            }
            return View(lesson);
        }

        public async Task<IActionResult> Create()
        {
            var courseVM = new AddCourseVm();
            var user = await _signInManager.UserManager.GetUserAsync(User);
            if (user.IssystemAdmin)
            {
                var teachers = await _teacherservice.GetAllAsync();
                courseVM.Teachers = teachers.ToList();
                ViewBag.TeacherList = new SelectList(courseVM.Teachers, "Id", "Name");
            }
            else
            {
                var teacher = await _teacherservice.GetByIdUser(user.Id);
                if (teacher!=null)
                {
                    courseVM.Teachers = new List<Teacher> { teacher};
                    ViewBag.TeacherList = new SelectList(courseVM.Teachers, "Id", "Name");
                    return View(courseVM);
                }
            }
            ModelState.AddModelError(string.Empty, "User Is not Teacher");
            return RedirectToAction("Index");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,IsDeleted,IsActive,CreatedOn")] Course lesson)
        {
            var user = await _signInManager.UserManager.GetUserAsync(User);
            var teacherId = (await _teacherservice.GetByIdUser(user.Id)).Id;
            lesson.CreatedOn=DateTime.Now;
            lesson.IsDeleted=false;
            lesson.TeacherId=teacherId;
            await _courseService.AddAsync(lesson);
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var lesson = await _courseService.GetByIdAsync(id);
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
                await _courseService.UpdateAsync(lesson);
                return RedirectToAction(nameof(Index));
            }
            return View(lesson);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var lesson = await _courseService.GetByIdAsync(id);
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
            await _courseService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> MyCourses()
        {
            var user = await _signInManager.UserManager.GetUserAsync(User);
            var teacher = await _teacherservice.GetByIdUser(user.Id);
            var courses =await _courseService.GetAllByTeacherId(teacher.Id);
            if (courses.Count()==0) { ModelState.AddModelError(string.Empty, "You Dont Have any Course"); }
            return View(courses);

           
        }
    }
}
