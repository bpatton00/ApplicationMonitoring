using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ApplicationVersionTracking.Data;
using ApplicationVersionTracking.Models;

namespace ApplicationVersionTracking.Pages.Applications
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationVersionTracking.Data.ApplicationContext _context;

        public IndexModel(ApplicationVersionTracking.Data.ApplicationContext context)
        {
            _context = context;
        }

        public IList<Application> Application { get;set; }

        public async Task OnGetAsync(string applicationName)
        {
            var res = from m in _context.Applications
                         .Include(a => a.Project)
                        .Include(a => a.StatusType)
                        .OrderBy(a => a.Name)
                         select m;

            if (!String.IsNullOrEmpty(applicationName))
            {
                res = res.Where(s => s.Name.Contains(applicationName));
            }

            Application = await res.ToListAsync();
        }
    }
}
