using Microsoft.AspNetCore.Mvc;
using MVCMonitoring.Data;
using MVCMonitoring.Models;

namespace MVCMonitoring.Controllers
{
    public class StationGraphController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context = context;
        public ActionResult Graph()
        {
            var stations = _context.Stations.ToList();
            var data = _context.Stations.Join(
                _context.Measurements,
                station => station.Id,
                measurement => measurement.StationId,
                (station, measurement) => new StationMeasurementModel
                {
                    Station = station,
                    Measurement = measurement
                }).ToList();

            ViewBag.Stations = stations;
            return View(data);
        }

        public ActionResult GetGraphData(int stationId)
        {
            var data = _context.Measurements
                .Where(m => m.StationId == stationId)
                .Select(m => new
                {
                    m.DateTime,
                    m.Station.FloodLevel,
                    m.Station.DroughtLevel,
                    m.WaterLevel
                })
                .ToList();

            return Json(data);
        }
    }
}