using Microsoft.AspNetCore.Mvc;
using MVCMonitoring.Models;
using System.Diagnostics;

namespace MVCMonitoring.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
        {
            var images = Directory.EnumerateFiles("./wwwroot/Images/")
                .Where(file => file.EndsWith(".jpg") || file.EndsWith(".png"))
                .Select(file => Path.GetFileName(file))
                .Where(name => name.StartsWith("Img (") && name.EndsWith(").jpg"))
                .ToList();

            return View(images);
        }

        [Route("About")]
        public IActionResult About()
        {

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        [Route("Error")]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}