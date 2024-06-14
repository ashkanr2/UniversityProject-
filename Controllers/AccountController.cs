using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UniversityProject.Entities;
using UniversityProject.Models;
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
        public async Task<IActionResult> Login(LoginViewModel model, CancellationToken cancellationToken)
        {
            if (model == null || string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.Password))
            {
                // Log and handle invalid model state
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.UserName);

            if (user == null)
            {
                // Log and handle user not found scenario
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }

            var passwordCheck = await _userManager.CheckPasswordAsync(user, model.Password);

            if (passwordCheck)
            {
                // Ensure user.UserName is not null
                if (string.IsNullOrEmpty(user.UserName))
                {
                    // Log and handle scenario where UserName is null
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }

                var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, true, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    // Log and handle failed login attempt
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }
            else
            {
                // Log and handle incorrect password scenario
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User model, string Password, CancellationToken cancellationToken)
        {


            model.EmailConfirmed=true;
            model.PhoneNumberConfirmed=true;
            model.CreatedOn=DateTime.Now;
            model.HelpPassword=Password;
            var result = await _userManager.CreateAsync(model, Password);
            ViewBag.Error = result.Errors.ToList().ToString();

            return View(model);

        }


    }
}
