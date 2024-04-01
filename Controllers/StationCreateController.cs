using Microsoft.AspNetCore.Mvc;
using MVCMonitoring.Data;
using MVCMonitoring.Models;

namespace MVCMonitoring.Controllers
{
    [ApiController]
    [Route("Create")]
    public class StationCreateController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StationCreateController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("~/api/add-station")]
        public IActionResult CreateStation(MonitoringStation station)
        {
            if (!ModelState.IsValid)
            {
                return View(station);
            }

            var existingStation = _context.Stations.FirstOrDefault(x => x.Title.Equals(station.Title));

            if (existingStation != null)
            {
                existingStation.FloodLevel = station.FloodLevel;
                existingStation.DroughtLevel = station.DroughtLevel;
                existingStation.TimeOutInMinutes = station.TimeOutInMinutes;

                _context.SaveChanges();

                return Ok("Station data has been updated.");
            }

            _context.Stations.Add(station);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}

//return RedirectToAction("Index");
