using Microsoft.AspNetCore.Mvc;

namespace MVCMonitoring.Controllers
{
    [Route("Gallery")]
    public class StationGalleryController : Controller
    {
        public IActionResult Gallery()
        {
            var images = Directory.EnumerateFiles("./wwwroot/Gallery/")
                .Where(file => file.EndsWith(".jpg") || file.EndsWith(".png"))
                .Select(file => Path.GetFileName(file))
                .ToList();

            var random = new Random();
            var shuffledImages = images.OrderBy(x => random.Next()).ToList();

            return View(shuffledImages);
        }
    }
}