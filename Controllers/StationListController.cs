using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCMonitoring.Data;


namespace MVCMonitoring.Controllers
{
    [ApiController]
    [Route("List_of_Stations")]
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
