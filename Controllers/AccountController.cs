using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UniversityProject.Entities;
using UniversityProject.Interfaces;
using UniversityProject.Models;
using UniversityProject.Services;

namespace UniversityProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailService _emailService;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
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
                    var userLoginInfo = new UserLoginInfo("DefaultProvider", user.Email, "DefaultProvider");
                    var addLoginResult = await _userManager.AddLoginAsync(user, userLoginInfo);

                    var loginTime = DateTime.Now;
                    var userIp = HttpContext.Connection.RemoteIpAddress.ToString();
                    var emailMessage = $"\n\nHello, you have logged into your account on {loginTime.ToShortDateString()} " +
                                       $"\n\nTime: {loginTime.ToShortTimeString()} " +
                                       $"\n\nIP Address: {userIp}" +
                                       $"\n\n (University project By Erfan And Ashkan)";


                    await _emailService.SendEmailAsync(user.Email, "Login Notification", emailMessage);

                    TempData["AlertMessage"] = $" Hello  {user.UserName}  Login successful.";
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
