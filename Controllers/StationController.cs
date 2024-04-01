using Microsoft.AspNetCore.Mvc;
using MVCMonitoring.Data;
using MVCMonitoring.Models;

namespace MVCMonitoring.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IActionResult Create()
        {
            return View();
        }

        public StationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("get-stations")]
        public IActionResult GetListOfStations()
        {
            var stations = _context.Stations.ToList();
            return Ok(stations);
        }

        [HttpPost("add-stations")]
        public IActionResult AddStation(Station station)
        {
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

            int newStationId = station.Id;

            return CreatedAtAction(nameof(GetStation), new { id = newStationId }, new
            {
                message = "Station has been successfully added",
                location = $"/api/station/{newStationId}"
            });
        }

        [HttpGet("get-station/{id}")]
        public IActionResult GetStation(int id)
        {
            var station = _context.Stations.Find(id);
            if (station == null)
            {
                return NotFound();
            }

            return Ok(station);
        }

        [HttpPost]
        public IActionResult CreateStation(Station station)
        {
            if (!ModelState.IsValid)
            {
                return View(station);
            }

            _context.Stations.Add(station);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index)); 
        }
    }
}