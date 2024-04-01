using Microsoft.AspNetCore.Mvc;
using MVCMonitoring.Data;

namespace MVCMonitoring.Controllers
{
    [ApiController]
    [Route("List")]
    public class StationListController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StationListController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult List()
        {
            ViewBag.Stations = _context.Stations.ToList();
            return View("List");
        }
    }
}
