﻿using Microsoft.AspNetCore.Authorization;
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

        public async Task<IActionResult> Index(string query = null)
        {
            IEnumerable<Course> courses = null;
            var courseListVMs = new List<CourseListVM>();
            var user = await _signInManager.UserManager.GetUserAsync(User);
            if (!string.IsNullOrEmpty(query) && query.Length < 3)
            {
                // Handle cases where the query is too short or empty
                ViewBag.ErrorMessagequery =null;
                ViewBag.ErrorMessagequery = "Search query must be at least 3 characters long.";
            }
            if (query != null && query.Length>=3)
            {
                courses = (await _courseService.SearchCourses(query)).OrderByDescending(c => c.CreatedOn).ToList();
                ViewBag.ErrorMessage = "Search Courses  Like "+query;

            }

            if (courses == null)
            {
                courses = (await _courseService.GetAllAsync()).OrderByDescending(c => c.CreatedOn).ToList();

            }

            foreach (var course in courses)
            {
                int studentNumbr = await _userCourseService.CalculateStudentCount(course.Id);
                var courseListVM = new CourseListVM
                {
                    Id = course.Id,
                    Name = course.Name,
                    Description = course.Description,
                    TeacherName = course.Teacher != null ? course.Teacher.Name : null, // Assuming Teacher has a Name property
                    Cost = course.Cost,
                    IsDeleted = course.IsDeleted,
                    IsActive = course.IsActive,
                    CreatedOn = course.CreatedOn,
                    Image = course.Image,
                    ImageId = course.ImageId,
                    Days = course.Days,
                    StartTime = course.StartTime,
                    EndTime=course.EndTime,
                    StartDate = course.StartDate,
                    EndDate = course.EndDate,
                    IsExist= user==null ? true : await _userCourseService.CourseIsExistForUser(user.Id, course.Id),
                    StudentNumber = studentNumbr,

                };
                courseListVMs.Add(courseListVM);
            }
            return View(courseListVMs);
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
                    courseVM.Teachers = new List<Teacher> { teacher };
                    ViewBag.TeacherList = new SelectList(courseVM.Teachers, "Id", "Name");
                    return View(courseVM);
                }
            }
            TempData["AlertMessage"] = "User Is not Teacher";
            return RedirectToAction("Index");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddCourseVm addCourseVM)
        {
            var courseVM = new AddCourseVm();
            var user = await _signInManager.UserManager.GetUserAsync(User);

            if (!ModelState.IsValid)
            {
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
                        courseVM.Teachers = new List<Teacher> { teacher };
                        ViewBag.TeacherList = new SelectList(courseVM.Teachers, "Id", "Name");
                        return View(courseVM);
                    }
                }
                return View(addCourseVM);
            }
            var course = new Course();
            var teacherId = new Guid();

            if (user.IssystemAdmin)
            {
                teacherId=addCourseVM.SelectedTeacherId;
            }
            else
            {
                teacherId = (await _teacherservice.GetByIdUser(user.Id)).Id;
            }
            if ((addCourseVM.Image != null && addCourseVM.Image.Length > 0))
            {
                int imageType = 1;
                var imageTypeId = await _imageService.GetImageType(imageType);
                var result = await _imageService.AddAsync(addCourseVM.Image, imageTypeId);
                if (result.Isvalid)
                {
                    course.ImageId= result.resultId;
                }
            }
            var selectedDays = new List<DayOfWeek>();
            if (addCourseVM.Sunday) selectedDays.Add(DayOfWeek.Sunday);
            if (addCourseVM.Monday) selectedDays.Add(DayOfWeek.Monday);
            if (addCourseVM.Tuesday) selectedDays.Add(DayOfWeek.Tuesday);
            if (addCourseVM.Wednesday) selectedDays.Add(DayOfWeek.Wednesday);
            if (addCourseVM.Thursday) selectedDays.Add(DayOfWeek.Thursday);
            if (addCourseVM.Friday) selectedDays.Add(DayOfWeek.Friday);
            if (addCourseVM.Saturday) selectedDays.Add(DayOfWeek.Saturday);


            course.Days=selectedDays;
            course.StartTime=addCourseVM.StartTime;
            course.EndDate=addCourseVM.EndDate;
            course.StartDate=addCourseVM.StartDate;
            course.Cost=addCourseVM.Cost;
            course.Name=addCourseVM.Name;
            course.TeacherId=teacherId;
            course.IsActive=addCourseVM.IsActive;
            course.Description=addCourseVM.Description;
            course.EndTime=addCourseVM.EndTime;
            await _courseService.AddAsync(course);
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Edit(Guid id)
        {
           
            var course = await _courseService.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }


            

            // Map Course entity to AddCourseVm
            var addCourseVm = new AddCourseVm
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                SelectedTeacherId = course.TeacherId,
                Cost = course.Cost,
                IsActive = course.IsActive,
                StartDate = course.StartDate,
                EndDate = course.EndDate,
                StartTime = course.StartTime,
                EndTime = course.EndTime,
                Sunday = course.Days.Contains(DayOfWeek.Sunday),
                Monday = course.Days.Contains(DayOfWeek.Monday),
                Tuesday = course.Days.Contains(DayOfWeek.Tuesday),
                Wednesday = course.Days.Contains(DayOfWeek.Wednesday),
                Thursday = course.Days.Contains(DayOfWeek.Thursday),
                Friday = course.Days.Contains(DayOfWeek.Friday),
                Saturday = course.Days.Contains(DayOfWeek.Saturday),
                //Teachers = await _teacherService.GetAllTeachersAsync() // Assuming you have a method to get all teachers
            };
            var teacher = await _teacherservice.GetByIdAsync(course.TeacherId);
            if (teacher!=null)
            {
                addCourseVm.Teachers = new List<Teacher> { teacher };
                ViewBag.TeacherList = new SelectList(addCourseVm.Teachers, "Id", "Name");
                return View(addCourseVm);
            }
            return View(addCourseVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, AddCourseVm addCourseVm)
        {
            if (id != addCourseVm.Id)
            {
                return NotFound();
            }

            // Map AddCourseVm to Course entity
            var course = await _courseService.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            course.Name = addCourseVm.Name;
            course.Description = addCourseVm.Description;
            course.TeacherId = addCourseVm.SelectedTeacherId;
            course.Cost = addCourseVm.Cost;
            course.IsActive = addCourseVm.IsActive;
            course.StartDate = addCourseVm.StartDate;
            course.EndDate = addCourseVm.EndDate;
            course.StartTime = addCourseVm.StartTime;
            course.EndTime = addCourseVm.EndTime;

            course.Days = new List<DayOfWeek>();
            if (addCourseVm.Sunday) course.Days.Add(DayOfWeek.Sunday);
            if (addCourseVm.Monday) course.Days.Add(DayOfWeek.Monday);
            if (addCourseVm.Tuesday) course.Days.Add(DayOfWeek.Tuesday);
            if (addCourseVm.Wednesday) course.Days.Add(DayOfWeek.Wednesday);
            if (addCourseVm.Thursday) course.Days.Add(DayOfWeek.Thursday);
            if (addCourseVm.Friday) course.Days.Add(DayOfWeek.Friday);
            if (addCourseVm.Saturday) course.Days.Add(DayOfWeek.Saturday);

            if (addCourseVm.Image != null && addCourseVm.Image.Length > 0)
            {
                int imageType = 1; // Assuming a specific image type
                var imageTypeId = await _imageService.GetImageType(imageType);
                var result = await _imageService.AddAsync(addCourseVm.Image, imageTypeId);
                if (result.Isvalid)
                {
                    course.ImageId = result.resultId;
                }
            }

            await _courseService.UpdateAsync(course);
            return RedirectToAction(nameof(Index));
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
                    StudentNumber = studentNumbr,
                    EndTime= course.EndTime,
                    Days=course.Days

                };

                courselistvm.Add(courseVM);
            }

            return View(courselistvm);

        }
        [HttpPost]
        public async Task<IActionResult> AddToMyCourse(Guid courseId)
        {
            var user = await _signInManager.UserManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["ErrorMessage"] = "User not found.";
                return RedirectToAction("Index");
            }

            try
            {
                // Check if the user can add the course
                var checkResult = _courseService.CanUserAddCourse(user.Id, courseId);

                if (checkResult.StartsWith("Error"))
                {
                    TempData["ErrorMessage"] = checkResult.ToString();
                    return RedirectToAction("Index"); // Redirect to course list or display an error message
                }

                // Proceed to add the course for the user
                var enrollmentResult = await _userCourseService.AddCourseForUser(user.Id, courseId);

                if (!enrollmentResult)
                {
                    TempData["ErrorMessage"] = "Failed to enroll in the course.";
                    return RedirectToAction("Index"); // Redirect to course list or display an error message
                }

                TempData["SuccessMessage"] = "Successfully enrolled in the course.";
                return RedirectToAction("Index"); // Redirect to course list or display a success message
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                TempData["ErrorMessage"] = "An error occurred while enrolling in the course.";
                return RedirectToAction("Index"); // Redirect to course list or display an error message
            }
        }



    }
}

