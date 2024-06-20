using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UniversityProject.Entities;
using UniversityProject.Models;

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
            if (_signInManager.IsSignedIn(User))
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Login");
            }
           return View();
           
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.Password))
            {
                TempData["AlertMessage"] = "Invalid login attempt.";
                TempData["AlertType"] = "danger";
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.UserName);

            if (user == null)
            {
                user = await _userManager.FindByNameAsync(model.UserName);
                if (user == null)
                {
                    TempData["AlertMessage"] = "Invalid login attempt.";
                    TempData["AlertType"] = "danger";
                    return View(model);
                }
            }

            var passwordCheck = await _userManager.CheckPasswordAsync(user, model.Password);

            if (passwordCheck)
            {
                var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, true, false);

                if (result.Succeeded)
                {
                    TempData["AlertMessage"] = "Login successful.";
                    TempData["AlertType"] = "success";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["AlertMessage"] = "Invalid login attempt.";
                    TempData["AlertType"] = "danger";
                    return View(model);
                }
            }
            else
            {
                TempData["AlertMessage"] = "Invalid login attempt.";
                TempData["AlertType"] = "danger";
                return View(model);
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User model, string Password)
        {
            model.EmailConfirmed = true;
            model.PhoneNumberConfirmed = true;
            model.CreatedOn = DateTime.Now;
            model.Birthdate = DateTime.Now;
            model.HelpPassword = Password;
            model.IsModified = true;

            var result = await _userManager.CreateAsync(model, Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(model, "student");
                TempData["AlertMessage"] = "Registration successful.";
                TempData["AlertType"] = "success";
                return RedirectToAction("Login", "Account");
            }
            else
            {
                TempData["AlertMessage"] = string.Join("; ", result.Errors.Select(e => e.Description));
                TempData["AlertType"] = "danger";
                return View(model);
            }
        }
    }
}
