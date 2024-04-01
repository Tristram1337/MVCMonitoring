using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVCMonitoring.Data;
using MVCMonitoring.Models;

namespace MVCMonitoring.Views.StationViews
{
	public class Create(ApplicationDbContext context) : PageModel
	{

        [BindProperty]
        public Station Station { get; set; } = new Station();
        private readonly ApplicationDbContext _context = context;

        public IActionResult OnPost()
		{
			//
			if (ModelState.IsValid)
			{
				return Page();
			}

			if (_context.Stations.Where(x => x.Title.Equals(Station.Title)).Any())
			{
				return StatusCode(400,
					new JsonResult("Duplicate values are not allowed."));
			}
			//

			_context.Stations.Add(Station);
			_context.SaveChanges();

			return RedirectToPage("/Index");
		}
	}
}