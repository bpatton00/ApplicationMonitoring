using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ApplicationVersionTracking.Data;
using ApplicationVersionTracking.Models;

namespace ApplicationVersionTracking.Pages.Events
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationVersionTracking.Data.ApplicationContext _context;

        public DetailsModel(ApplicationVersionTracking.Data.ApplicationContext context)
        {
            _context = context;
        }

        public Event Event { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Event = await _context.Events
                .Include(a => a.Application)
                .Include(a => a.EnvironmentType)
                .Include(a => a.EventType)
                .Include(a => a.Server).SingleOrDefaultAsync(m => m.ID == id);

            if (Event == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
