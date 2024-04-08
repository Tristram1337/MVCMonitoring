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
                Location = station.Location,
                FloodLevel = station.FloodLevel,
                DroughtLevel = station.DroughtLevel,
                TimeOutInMinutes = station.TimeOutInMinutes
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
            existingStation.FloodLevel = updatedStation.FloodLevel;
            existingStation.DroughtLevel = updatedStation.DroughtLevel;
            existingStation.TimeOutInMinutes = updatedStation.TimeOutInMinutes;
            _context.SaveChanges();

            return Ok(existingStation);
        }

        [HttpDelete("delete-station/{id}")]
        public IActionResult DeleteStation(int id)
        {
            var existingStation = _context.Stations.Find(id);

            if (existingStation == null)
            {
                return NotFound("Station not found.");
            }

            _context.Stations.Remove(existingStation);
            _context.SaveChanges();

            return Ok("Station deleted successfully.");
        }

        // MEASUREMENTS //

        [HttpGet("get-all-measurements")]
        public IActionResult GetAllMeasurements()
        {
            var allMeasurements = _context.Measurements.ToList();

            if (allMeasurements.Count == 0)
            {
                return NotFound("No measurements found.");
            }

            return Ok(allMeasurements);
        }

        [HttpGet("get-measurement/{measurementId}")]
        public IActionResult GetMeasurement(int measurementId)
        {
            var existingMeasurement = _context.Measurements.Find(measurementId);

            if (existingMeasurement == null)
            {
                return NotFound("Measurement not found.");
            }

            return Ok(existingMeasurement);
        }

        [HttpPost("add-measurements/{stationId}")]
        public IActionResult AddMeasurement(int stationId, Measurement measurement)
        {
            var existingStation = _context.Stations.Find(stationId);

            if (existingStation == null)
            {
                return NotFound("Station not found.");
            }

            var newMeasurement = new Measurement
            {
                WaterLevel = measurement.WaterLevel,
                DateTime = DateTime.UtcNow,
                StationId = stationId
            };

            _context.Measurements.Add(newMeasurement);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetStation), new { id = newMeasurement.Id }, newMeasurement);
        }

        [HttpPut("update-measurement/{measurementId}")]
        public IActionResult UpdateMeasurement(int measurementId, Measurement updatedMeasurement)
        {
            var existingMeasurement = _context.Measurements.Find(measurementId);

            if (existingMeasurement == null)
            {
                return NotFound("Measurement not found.");
            }

            existingMeasurement.WaterLevel = updatedMeasurement.WaterLevel;
            existingMeasurement.DateTime = DateTime.UtcNow;

            _context.SaveChanges();

            return Ok("Measurement updated successfully.");
        }

        [HttpDelete("delete-measurement/{measurementId}")]
        public IActionResult DeleteMeasurement(int measurementId)
        {
            var existingMeasurement = _context.Measurements.Find(measurementId);

            if (existingMeasurement == null)
            {
                return NotFound("Measurement not found.");
            }

            _context.Measurements.Remove(existingMeasurement);
            _context.SaveChanges();

            return Ok("Measurement deleted successfully.");
        }
    }
}