using Microsoft.AspNetCore.Mvc;
using MVCMonitoring.Data;
using MVCMonitoring.Models;

namespace MVCMonitoring.Controllers
{
    [ApiController]
    [Route("api/")]
    public class StationController : Controller
    {
        private ApplicationDbContext _context { get; set; }

        public IActionResult Create()
        {

            return View();
        }

        public StationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("get-stations")]
        public IActionResult GetListOfStations()
        {
            var list = _context.Stations.ToList();
            return StatusCode(200, new JsonResult(list));
        }

        [HttpPost]
        [Route("add-stations")]
        public IActionResult AddStation(Station station)
        {
            if (_context.Stations.Where(x => x.Title.Equals(station.Title)).Any())
            {
                return StatusCode(400,
                    new JsonResult("Duplicate values are not allowed."));
            }

            _context.Stations.Add(station);
            _context.SaveChanges();

            return StatusCode(200, new JsonResult("Station has been successfully added"));
        }
    }
}