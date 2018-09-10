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
    public class ByInstaller : PageModel
    {
        private readonly ApplicationVersionTracking.Data.ApplicationContext _context;

        public ByInstaller(ApplicationVersionTracking.Data.ApplicationContext context)
        {
            _context = context;
        }
        public string CurrentFilter { get; set; }
        public PaginatedList<Event> Event { get;set; }
        public IList<EnvironmentType> Environment { get; set; }
        
        public async Task<IActionResult> OnGetAsync(string Installer, int? Env, int? pageIndex)
        {
            int pageSize = 20;
            CurrentFilter = Installer;

            Event = await PaginatedList<Event>.CreateAsync(_context.Events
                .Include(a => a.Application)
                .Include(a => a.EnvironmentType)                             
                .Include(a => a.EventType)
                .Include(a => a.Server)
                .Where(a => a.Installer == Installer)
                .OrderByDescending(a => a.EventDate)
                , pageIndex ?? 1, pageSize);

            if (Env > 0)
            {
                Event = await PaginatedList<Event>.CreateAsync(_context.Events
                .Include(a => a.Application)
                .Include(a => a.EnvironmentType)
                .Include(a => a.EventType)
                .Include(a => a.Server)
                .Where(a => a.Installer == Installer)
                .Where(a => a.EnvironmentTypeID == Env)
                .OrderByDescending(a => a.EventDate)
                , pageIndex ?? 1, pageSize);
            }

            return Page();
        }
    }
}
