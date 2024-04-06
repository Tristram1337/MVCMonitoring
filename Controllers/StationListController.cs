using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCMonitoring.Data;

namespace MVCMonitoring.Controllers
{
    public class StationListController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context = context;

        public IActionResult List()
        {
            var stationsWithMeasurements = _context.Stations
                .Include(s => s.Measurements)
                .ToList();

            return View(stationsWithMeasurements);
        }
    }
}
