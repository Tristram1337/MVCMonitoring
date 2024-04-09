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
            var data = _context.Stations.Join(
                _context.Measurements,
                station => station.Id,
                measurement => measurement.StationId,
                (station, measurement) => new StationMeasurementModel
                {
                    Station = station,
                    Measurement = measurement

                }).ToList();

            return View(data);
        }

    }
}
