using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UniversityProject.Entities;

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
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);

                if (result.Succeeded)
                {

                    var user = await _userManager.FindByNameAsync(model.UserName);
                 
                    //foreach (var item in roles.Result)
                    //{
                    //    //if(item == "Admin")
                    //    //{
                    //    //    return RedirectToAction("Index", "Home", new { area = "Admin" });
                    //    //}
                    //    //if(item == "Customer")
                    //    //{
                    //    //    return RedirectToAction("Index", "Home", new { area = "Admin" });
                    //    //}
                    //    if (item == "Seller")
                    //    {
                    //        return RedirectToAction("Index", "Home", new { area = "Seller" });
                    //    }

                    //}

                }
                ModelState.AddModelError(string.Empty, "خطا در لاگین ");
            }

            return View(model);
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                //if (model.Image != null && model.Image.Length > 0)
                //{
                //    var fileNameWithoutExtension = DateTime.Now.Ticks.ToString();

                //    //var wwwrootPath = _hostingEnvironment.WebRootPath;
                //    var uploadPath = Path.Combine(wwwrootPath, "uploads");

                //    if (!Directory.Exists(uploadPath))
                //    {
                //        Directory.CreateDirectory(uploadPath);
                //    }

                //    // Get the original file extension
                //    var fileExtension = Path.GetExtension(model.Image.FileName);

                //    // Construct the new file name
                //    var fileName = fileNameWithoutExtension + fileExtension;

                //    var filePath = Path.Combine(uploadPath, fileName);

                //    using (var stream = new FileStream(filePath, FileMode.Create))
                //    {
                //        await model.Image.CopyToAsync(stream);
                //    }


                //    var imageNameInDatabase = fileNameWithoutExtension + ".png" + "_+_" + Path.GetFileNameWithoutExtension(model.Image.FileName).Replace(".", "");

                //    var user = new AppUser
                //    {
                //        UserProfileImageId = (await _imageAppservice.Upload(imageNameInDatabase, true, cancellationToken)),
                //        Name = model.Name,
                //        LastName = model.LastName,
                //        Email = model.Email,
                //        UserName = model.Email,
                //        Address = model.Address,
                //        CreatAt=DateTime.Now,
                //        EmailConfirmed=true,
                //    };

                //    var result = await _userManager.CreateAsync(user, model.Password);
                //}
            }

            return View(model);
        }


    }
}
