using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
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
        private readonly UserManager<User> _userManager;
        private readonly IUserCourseService _userCourseService;
        public CourseController(
            ICourseService courseService,
            UserManager<User> userManager,
            ICourseService lessonService,
            ITeacherService teacherservic, 
            SignInManager<User> signInManager,
            IUserCourseService userCourseService)
        {
            _courseService = lessonService;
            _teacherservice=teacherservic;
            _signInManager = signInManager;
            _userManager = userManager;
            _courseService=courseService;
            _userCourseService = userCourseService;
        }

        public async Task<IActionResult> Index(string query=null)
        {
            IEnumerable<Course> courses = null;
            if (!string.IsNullOrEmpty(query) && query.Length < 3)
            {
                // Handle cases where the query is too short or empty
                ViewBag.ErrorMessage = "Search query must be at least 3 characters long.";
                return View();
            }
            if (query != null && query.Length>=3) 
            {
                courses = (await _courseService.SearchCourses(query));
                ViewBag.ErrorMessage = "Search Courses  Like "+query;

            }
           
            if (courses == null)
            {
                courses = await _courseService.GetAllAsync();
            }

            return View(courses);
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
        public async Task<IActionResult> Create([Bind("Id,Name,Description,IsDeleted,IsActive,CreatedOn,Cost")] Course course)
        {
            var user = await _signInManager.UserManager.GetUserAsync(User);
            var teacherId = (await _teacherservice.GetByIdUser(user.Id)).Id;
            course.CreatedOn=DateTime.Now;
            course.IsDeleted=false;
            course.TeacherId=teacherId;
            await _courseService.AddAsync(course);
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
            if(user.IsTeacher)
            {
                var teacher = await _teacherservice.GetByIdUser(user.Id);
                var courses = await _courseService.GetAllByTeacherId(teacher.Id);
                if (courses.Count()==0) { ModelState.AddModelError(string.Empty, "You Dont Have any Course"); }
                return View(courses);
            }
            else
            {
                var courses = await _userCourseService.GetAllByUserId(user.Id);
                if (courses.Count()==0) { ModelState.AddModelError(string.Empty, "You Dont Have any Course"); }
                return View(courses);
            }
           
        }
        [HttpPost]
        public async Task<IActionResult> AddToMyCourse(Guid courseId)
        {
            var user = await _signInManager.UserManager.GetUserAsync(User);
            try
            {
                var enrollmentResult = await _userCourseService.AddCourseForUser(user.Id, courseId);
                if (!enrollmentResult)
                {
                    ModelState.AddModelError(string.Empty, "Failed to enroll in the course.");
                    // Redirect to course list or display an error message
                }

                return RedirectToAction("Index"); // Redirect to course list or display a success message
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                ModelState.AddModelError(string.Empty, "An error occurred while enrolling in the course.");
                return RedirectToAction("Index"); // Redirect to course list or display an error message
            }
        }
        public int MyProperty { get; set; }
    }
}

