using Microsoft.AspNetCore.Mvc;
using MVCMonitoring.Data;
using MVCMonitoring.Models;

namespace MVCMonitoring.Controllers
{
    [ApiController]
    [Route("Operations")]
    public class StationOperationController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context = context;

        [Route("CreateStation")]
        public IActionResult CreateStation()
        {
            return View();
        }

        [Route("UpdateStation")]
        public IActionResult UpdateStation()
        {
            return View();
        }

        [Route("DeleteStation")]
        public IActionResult DeleteStation()
        {
            return View();
        }

        [HttpPost("CreateStationAction")]
        public async Task<IActionResult> CreateStationAction([FromForm] MonitoringStation station)
        {
            if (!ModelState.IsValid)
            {
                return View(station);
            }

            var existingStation = _context.Stations.FirstOrDefault(x => x.Title.Equals(station.Title));

            if (existingStation != null)
            {
                return Conflict(new { message = "A station with this title already exists." });
            }

            var newStation = new MonitoringStation
            {
                Title = station.Title,
                Location = station.Location,
                FloodLevel = station.FloodLevel,
                DroughtLevel = station.DroughtLevel,
                TimeOutInMinutes = station.TimeOutInMinutes
            };

            _context.Stations.Add(station);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Station created successfully." });
        }

        [HttpPost("UpdateStationAction")]
        public async Task<IActionResult> UpdateStationAction([FromForm] MonitoringStation station)
        {
            if (!ModelState.IsValid)
            {
                return View(station);
            }

            var existingStation = _context.Stations.FirstOrDefault(x => x.Id == station.Id);

            if (existingStation == null)
            {
                return NotFound(new { message = "No station found with this ID." });
            }

            existingStation.Title = station.Title;
            existingStation.Location = station.Location;
            existingStation.FloodLevel = station.FloodLevel;
            existingStation.DroughtLevel = station.DroughtLevel;
            existingStation.TimeOutInMinutes = station.TimeOutInMinutes;

            _context.Stations.Update(existingStation);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Station updated successfully." });
        }

        [HttpPost("DeleteStationAction")]
        public async Task<IActionResult> DeleteStationAction([FromForm] int id)
        {
            var existingStation = _context.Stations
                .Where(s => s.Id == id)
                .Select(s => new { s.Id, s.Location })
                .FirstOrDefault();

            if (existingStation == null)
            {
                return NotFound(new { message = "No station found with this ID." });
            }

            var stationToDelete = new MonitoringStation { Id = existingStation.Id, Location = existingStation.Location };
            _context.Stations.Remove(stationToDelete);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Station successfully deleted." });
        }
    }
}