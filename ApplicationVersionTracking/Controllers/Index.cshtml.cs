using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ApplicationVersionTracking.Data;
using ApplicationVersionTracking.Models;

namespace ApplicationVersionTracking.Controllers
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationVersionTracking.Data.ApplicationContext _context;

        public IndexModel(ApplicationVersionTracking.Data.ApplicationContext context)
        {
            _context = context;
        }

        public IList<Application> Application { get;set; }

        public async Task OnGetAsync()
        {
            Application = await _context.Applications.ToListAsync();
        }
    }
}
