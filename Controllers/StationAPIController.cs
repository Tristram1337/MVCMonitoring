using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCMonitoring.Data;
using MVCMonitoring.Models;

namespace MVCMonitoring.Controllers
{
    [ApiController]
    [Route("api")]
    public class StationAPIController(ApplicationDbContext context) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;

        [HttpGet("get-stations")]
        public IActionResult GetStations()
        {
            var stations = _context.Stations.Include(s => s.Measurements).ToList();
            return Ok(stations);
        }

        [HttpPost("add-stations")]
        public IActionResult AddStation(MonitoringStation station)
        {
            var existingStation = _context.Stations.FirstOrDefault(x => x.Title.Equals(station.Title));

            if (existingStation != null)
            {
                return Conflict("A station with this title already exists.");
            }

            var newStation = new MonitoringStation
            {
                Title = station.Title,
                Location = station.Location
            };

            _context.Stations.Add(newStation);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetStation), new { id = newStation.Id }, newStation);
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
        public IActionResult UpdateStation(int id, MonitoringStation updatedStation)
        {
            var existingStation = _context.Stations.Find(id);

            if (existingStation == null)
            {
                return NotFound();
            }

            existingStation.Title = updatedStation.Title;
            existingStation.Location = updatedStation.Location;
            _context.SaveChanges();

            return Ok(existingStation);
        }

        [HttpPost("add-measurements/{stationId}")]
        public IActionResult AddMeasurements(int stationId, ICollection<Measurement> measurements)
        {
            var existingStation = _context.Stations.Find(stationId);

            if (existingStation == null)
            {
                return NotFound("Station not found.");
            }

            foreach (var measurement in measurements)
            {
                measurement.StationId = stationId;

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _context.Measurements.Add(measurement);
            }

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to add measurements: {ex.Message}");
            }

            return Ok("Measurements added successfully.");
        }

    }
}