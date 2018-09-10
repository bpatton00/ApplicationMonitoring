using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ApplicationVersionTracking.Data;
using ApplicationVersionTracking.Models;

namespace ApplicationVersionTracking.Pages.Servers
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationVersionTracking.Data.ApplicationContext _context;

        public IndexModel(ApplicationVersionTracking.Data.ApplicationContext context)
        {
            _context = context;
        }

        public IList<Server> Server { get;set; }

        public async Task OnGetAsync(string ServerName)
        {

            var res = from m in _context.Servers
                         .Include(a => a.StatusType)
                        .OrderBy(a => a.Name)
                      select m;

            if (!String.IsNullOrEmpty(ServerName))
            {
                res = res.Where(s => s.Name.Contains(ServerName));
            }

            Server = await res.ToListAsync();          
        }
    }
}
