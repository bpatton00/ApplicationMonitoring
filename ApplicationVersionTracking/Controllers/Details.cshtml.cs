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
    public class DetailsModel : PageModel
    {
        private readonly ApplicationVersionTracking.Data.ApplicationContext _context;

        public DetailsModel(ApplicationVersionTracking.Data.ApplicationContext context)
        {
            _context = context;
        }

        public Application Application { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Application = await _context.Applications.SingleOrDefaultAsync(m => m.ID == id);

            if (Application == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
