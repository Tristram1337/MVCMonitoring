using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCMonitoring.Data;
using MVCMonitoring.Models;
using System.Diagnostics;

namespace MVCMonitoring.Controllers
{
    public class HomeController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context = context;

        public IActionResult Index()
        {
            var images = Directory.EnumerateFiles("./wwwroot/Images/")
                .Where(file => file.EndsWith(".jpg") || file.EndsWith(".png"))
                .Select(file => Path.GetFileName(file))
                .Where(name => name.StartsWith("Img (") && name.EndsWith(").jpg"))
                .ToList();

            ViewBag.Stations = new SelectList(_context.Stations, "Id", "Title");

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