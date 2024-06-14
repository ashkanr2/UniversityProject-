using Microsoft.AspNetCore.Mvc;
using UniversityProject.Entities;
using UniversityProject.Interfaces;
using UniversityProject.Models;

namespace UniversityProject.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IImageService _imageService;
        private readonly ITeacherService _teacherService;

        public UserController(IUserService userService, IImageService imageService, ITeacherService teacherService)
        {
            _userService = userService;
            _imageService = imageService;
            _teacherService=teacherService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _userService.GetAllAsync());
        }

        public async Task<IActionResult> CreateTeacher(Guid userId)
        {
            Teacher teacher = new Teacher();
            var user = await _userService.GetByIdAsync(userId);
            user.IsTeacher=true;
            _userService.UpdateAsync(user);
            if (user == null)
            {
                return RedirectToAction("index", "user");
            }
            teacher.UserId = user.Id;
            teacher.Name=user.Firstname + " " + user.Lastname;
            teacher.IsActive=true;
            await _teacherService.AddAsync(teacher);

            return RedirectToAction("Index");
            
            
        }

    }

}
