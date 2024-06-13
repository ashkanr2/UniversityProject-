using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UniversityProject.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UniversityProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> Login()
        {
            await _signInManager.SignOutAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(User model, CancellationToken cancellationToken)
        {
              var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);

            return RedirectToAction("Index", "Home");
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User model, CancellationToken cancellationToken)
        {


            var result = await _userManager.CreateAsync(model, model.Password);
            ViewBag.Error = result.Errors.ToList().ToString();

            return View(model);

        }


    }
}
