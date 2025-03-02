using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PetManagementSystem.Config;
using PetManagementSystem.Models;
using System.Diagnostics;

namespace PetManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // access ho raha  the Singleton instance
            var config = AppConfig.Instance;

            // Use ho raha haiii configuration values
            ViewBag.ApplicationName = config.ApplicationName;
            ViewBag.MaxPetsAllowed = config.MaxPetsAllowed;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
