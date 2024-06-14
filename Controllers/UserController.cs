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

        public UserController(IUserService userService, IImageService imageService)
        {
            _userService = userService;
            _imageService = imageService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _userService.GetAllAsync());
        }

        
    }

}
