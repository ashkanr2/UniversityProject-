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
            _teacherService = teacherService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllAsync();
            return View(users);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var model = new EditUserVM
            {
                Id = user.Id,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Birthdate = user.Birthdate,
                IssystemAdmin = user.IssystemAdmin,
                IsDeleted = user.IsDeleted,
                IsModified = user.IsModified
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, EditUserVM model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _userService.UpdateAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await _userService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> CreateTeacher(Guid userId)
        {
            var user = await _userService.GetByIdAsync(userId);
            if (user == null)
            {
                return RedirectToAction("Index");
            }

            user.IsTeacher = true;
            await _userService.UpdateAsync(new EditUserVM
            {
                Id = user.Id,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Birthdate = user.Birthdate,
                IssystemAdmin = user.IssystemAdmin,
                IsDeleted = user.IsDeleted,
                IsModified = true
            });

            var teacher = new Teacher
            {
                UserId = user.Id,
                Name = user.Firstname + " " + user.Lastname,
                IsActive = true
            };

            await _teacherService.AddAsync(teacher);

            return RedirectToAction("Index");
        }
    }
}
