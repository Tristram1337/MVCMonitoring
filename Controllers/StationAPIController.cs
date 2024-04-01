using Microsoft.AspNetCore.Mvc;
using MVCMonitoring.Data;
using MVCMonitoring.Models;

namespace MVCMonitoring.Controllers
{
    [ApiController]
    [Route("api")]
    public class StationAPIController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StationAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("get-stations")]
        public IActionResult GetStations()
        {
            var stations = _context.Stations.ToList();
            return Ok(stations);


        }
        [HttpPost("add-stations")]
        public IActionResult AddStation(MonitoringStation station)
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

        [HttpPut("update-station/{id}")]
        public IActionResult UpdateStation(int id, Measurements newValue)
        {
            var existingStation = _context.Stations.Find(id);

            if (existingStation == null)
            {
                return NotFound();
            }

            if (newValue != null)
            {
                newValue.Station = existingStation;
                _context.Values.Add(newValue);
                _context.SaveChanges();
            }

            return Ok(existingStation);
        }
    }
}