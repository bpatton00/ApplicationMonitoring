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
    public class DetailsModel : PageModel
    {
        private readonly ApplicationVersionTracking.Data.ApplicationContext _context;

        public DetailsModel(ApplicationVersionTracking.Data.ApplicationContext context)
        {
            _context = context;
        }

        public Server Server { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Server = await _context.Servers
                .Include(x => x.StatusType)
                .SingleOrDefaultAsync(m => m.ID == id);

            if (Server == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
