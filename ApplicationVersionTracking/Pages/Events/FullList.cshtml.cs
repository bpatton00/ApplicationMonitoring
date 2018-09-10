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
    public class FullListModel : PageModel
    {
        private readonly ApplicationVersionTracking.Data.ApplicationContext _context;

        public FullListModel(ApplicationVersionTracking.Data.ApplicationContext context)
        {
            _context = context;
        }

        public IList<Event> Event { get;set; }
        public IList<EnvironmentType> Environment { get; set; }

        public async Task<IActionResult> OnGetAsync(int? App, int? Env)
        {
            if (App == null || Env == null)
            {
                return NotFound();
            }

            Event = await _context.Events
                .Include(a => a.Application)
                .Include(a => a.EnvironmentType)                             
                .Include(a => a.EventType)
                .Include(a => a.Server)
                .Where(a => a.ApplicationID == App && a.EnvironmentTypeID == Env)
                .OrderByDescending(a => a.EventDate)
                .ToListAsync();

            return Page();
        }
    }
}
