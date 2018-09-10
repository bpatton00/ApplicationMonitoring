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
    public class IndexModel : PageModel
    {
        private readonly ApplicationVersionTracking.Data.ApplicationContext _context;

        public IndexModel(ApplicationVersionTracking.Data.ApplicationContext context)
        {
            _context = context;
        }

        public IList<Event> Event { get;set; }

        public async Task OnGetAsync(string applicationName, int? Env)
        {

            var res = from m in _context.Events
                         .Include(a => a.Application)
                        .Include(a => a.EnvironmentType)
                        .Include(a => a.EventType)
                        .Include(a => a.Server)
                       .OrderByDescending(a => a.EventDate)
                      select m;

            if (!String.IsNullOrEmpty(applicationName))
            {
                res = res.Where(s => s.Application.Name.Contains(applicationName));
            }
            if (Env > 0)
            {
                res = res.Where(s => s.EnvironmentTypeID == Env);
            }
            Event = await res.ToListAsync();
        }
    }
}
