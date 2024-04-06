using Microsoft.AspNetCore.Mvc;
using MVCMonitoring.Data;
using MVCMonitoring.Models;

namespace MVCMonitoring.Controllers
{
    [ApiController]
    [Route("Create")]
    public class StationController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context = context;

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateStation([FromForm] MonitoringStation station)
        {
            if (!ModelState.IsValid)
            {
                return View(station);
            }

            var existingStation = _context.Stations.FirstOrDefault(x => x.Title.Equals(station.Title));

            if (existingStation != null)
            {
                return Conflict(new { message = "A station with this title already exists.", title = existingStation.Title, location = existingStation.Location });
            }

            _context.Stations.Add(station);
            _context.SaveChanges();

            return RedirectToAction("List", "StationList");
        }
    }
}