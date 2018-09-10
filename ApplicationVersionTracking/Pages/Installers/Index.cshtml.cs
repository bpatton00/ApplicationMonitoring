using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ApplicationVersionTracking.Data;
using ApplicationVersionTracking.Models;

namespace ApplicationVersionTracking.Pages.Installers
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationVersionTracking.Data.ApplicationContext _context;

        public IndexModel(ApplicationVersionTracking.Data.ApplicationContext context)
        {
            _context = context;
        }

        public IList<Event> Event { get;set; }
        public IList<EnvironmentType> Environment { get; set; }

        public async Task<IActionResult> OnGetAsync(string Installer)
        {        
            Event = await _context.Events
                .Include(a => a.Application)
                .Include(a => a.EnvironmentType)                             
                .Include(a => a.EventType)
                .Include(a => a.Server)                  
                .OrderByDescending(a => a.Installer)
                .ToListAsync();

            return Page();
        }
    }
}
