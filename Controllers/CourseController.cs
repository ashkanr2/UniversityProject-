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
        private readonly IImageService _imageService;
        public CourseController(
            IImageService imageService,
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
            _imageService=imageService; 
        }

        public async Task<IActionResult> Index(string query=null)
        {
            IEnumerable<Course> courses = null;
            
            if (!string.IsNullOrEmpty(query) && query.Length < 3)
            {
                // Handle cases where the query is too short or empty
                ViewBag.ErrorMessagequery = "Search query must be at least 3 characters long.";
            }
            if (query != null && query.Length>=3) 
            {
                courses = (await _courseService.SearchCourses(query)).OrderByDescending(c => c.CreatedOn);
                ViewBag.ErrorMessage = "Search Courses  Like "+query;

            }
           
            if (courses == null)
            {
                courses = (await _courseService.GetAllAsync()).OrderByDescending(c=>c.CreatedOn);
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
                return View(courseVM);
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
            TempData["AlertMessage"] = "User Is not Teacher";
            return RedirectToAction("Index");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( AddCourseVm addCourseVM )
        {
            
            var course = new Course();
            var teacherId = new Guid();
            var user = await _signInManager.UserManager.GetUserAsync(User);
            if(user.IssystemAdmin)
            {
                teacherId=addCourseVM.SelectedTeacherId;
            }
            else
            {
                teacherId = (await _teacherservice.GetByIdUser(user.Id)).Id;
            }
            if((addCourseVM.Image != null && addCourseVM.Image.Length > 0))
            {
                int imageType = 1;
                var imageTypeId = await _imageService.GetImageType(imageType);
                var result = await _imageService.AddAsync(addCourseVM.Image , imageTypeId);
                if (result.Isvalid)
                {
                    course.ImageId= result.resultId;
                }
            }
            course.Cost=addCourseVM.Cost;
            course.Name=addCourseVM.Name;
            course.TeacherId=teacherId;
            course.IsActive=addCourseVM.IsActive;
            course.Description=addCourseVM.Description;
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
            IEnumerable<Course> courses = null;
            var courselistvm = new List<CourseListVM>();
            var user = await _signInManager.UserManager.GetUserAsync(User);
            if (user.IsTeacher)
            {
                var teacher = await _teacherservice.GetByIdUser(user.Id);
                 courses = await _courseService.GetAllByTeacherId(teacher.Id);
                if (courses.Count()==0) { ModelState.AddModelError(string.Empty, "You Dont Have any Course"); }

            }
            else
            {
                courses = await _userCourseService.GetAllCoursesByUserId(user.Id);
                if (courses.Count()==0) { ModelState.AddModelError(string.Empty, "You Dont Have any Course"); }

            }
            //var courseListVm = await _courseService.GetAllUserCourses(user.Id);
            foreach (var course in courses)
            {
                int studentNumbr = await _userCourseService.CalculateStudentCount(course.Id);
                var courseVM = new CourseListVM
                {
                    Id = course.Id,
                    Name = course.Name,
                    Description = course.Description,
                    TeacherName = course.Teacher.Name,
                    Cost = course.Cost,
                    IsDeleted = course.IsDeleted,
                    IsActive = course.IsActive,
                    CreatedOn = course.CreatedOn,
                    Image = course.Image,
                    ImageId = course.ImageId,
                    StudentNumber = studentNumbr
                };

                courselistvm.Add(courseVM);
            }

            return View(courselistvm);

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

    }
}

