using Microsoft.AspNetCore.Mvc;
using MVCMonitoring.Data;
using MVCMonitoring.Models;

namespace MVCMonitoring.Controllers
{
    [ApiController]
    [Route("Operations")]
    public class MeasurementOperationController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context = context;

        [Route("CreateMeasurement")]
        public IActionResult CreateMeasurement()
        {
            return View();
        }

        [Route("UpdateMeasurement")]
        public IActionResult UpdateMeasurement()
        {
            return View();
        }

        [Route("DeleteMeasurement")]
        public IActionResult DeleteMeasurement()
        {
            return View();
        }

        [HttpPost("CreateMeasurementAction")]
        public IActionResult CreateMeasurementAction([FromForm] Measurement measurement)
        {
            var existingStation = _context.Stations.Find(measurement.StationId);

            if (existingStation == null)
            {
                return NotFound("Station not found.");
            }

            var newMeasurement = new Measurement
            {
                WaterLevel = measurement.WaterLevel,
                DateTime = DateTime.UtcNow,
                StationId = measurement.StationId
            };

            _context.Measurements.Add(newMeasurement);
            _context.SaveChanges();

            return RedirectToAction("List", "StationList");
        }

        [HttpPost("UpdateMeasurementAction")]
        public IActionResult UpdateMeasurementAction([FromForm] Measurement measurement)
        {
            var existingMeasurement = _context.Measurements.Find(measurement.Id);

            if (existingMeasurement == null)
            {
                return NotFound("Measurement not found.");
            }

            existingMeasurement.WaterLevel = measurement.WaterLevel;
            existingMeasurement.DateTime = DateTime.UtcNow;
            existingMeasurement.StationId = measurement.StationId;

            _context.SaveChanges();

            return RedirectToAction("List", "StationList");
        }

        [HttpPost("DeleteMeasurementAction")]
        public IActionResult DeleteMeasurementAction([FromForm] int id)
        {
            var existingMeasurement = _context.Measurements
                .Where(m => m.Id == id)
                .Select(m => new { m.Id, m.WaterLevel })
                .FirstOrDefault();

            if (existingMeasurement == null)
            {
                return NotFound("Measurement not found.");
            }

            var measurementToDelete = new Measurement { Id = existingMeasurement.Id, WaterLevel = existingMeasurement.WaterLevel };
            _context.Measurements.Remove(measurementToDelete);
            _context.SaveChanges();

            return RedirectToAction("List", "StationList");
        }
    }
}