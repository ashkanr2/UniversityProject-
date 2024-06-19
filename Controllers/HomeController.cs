using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UniversityProject.Interfaces;
using UniversityProject.Models;

namespace UniversityProject.Controllers
{
   
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICourseService _courseService;
        private readonly ITeacherService _teacherService;
        public HomeController(ILogger<HomeController> logger,
            ICourseService courseService , 
            ITeacherService teacherService
            )
        {
            _logger = logger;
            _courseService = courseService;
            _teacherService = teacherService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        //public async Task<IActionResult> Search(string query)
        //{
        //    if (string.IsNullOrEmpty(query) || query.Length < 3)
        //    {
        //        // Handle cases where the query is too short or empty
        //        ViewBag.ErrorMessage = "Search query must be at least 3 characters long.";
        //        return View("Index");
        //    }

        //    var courseIds = (await _courseService.SearchCourses(query)).Select(c=>c.Id);

        //    TempData["SearchResults"] = Newtonsoft.Json.JsonConvert.SerializeObject(courseIds);

        //    return RedirectToAction("Index", "Course");
        //}

    }
}
