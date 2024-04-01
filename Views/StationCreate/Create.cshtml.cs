//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using MVCMonitoring.Data;
//using MVCMonitoring.Models;

//namespace MVCMonitoring.Views.Station
//{
//    public class Create(ApplicationDbContext context) : PageModel
//    {

//        [BindProperty]
//        public MonitoringStation Station { get; set; } = new MonitoringStation();
//        private readonly ApplicationDbContext _context = context;

//        public IActionResult OnPost()
//        {
//            if (!ModelState.IsValid)
//            {
//                return Page();
//            }

//            if (_context.Stations.Any(x => x.Title.Equals(Station.Title)))
//            {
//                return BadRequest("Duplicate values are not allowed.");
//            }

//            _context.Stations.Add(Station);
//            _context.SaveChanges();

//            return Page();
//        }
//    }
//}