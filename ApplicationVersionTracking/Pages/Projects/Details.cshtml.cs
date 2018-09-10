using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ApplicationVersionTracking.Data;
using ApplicationVersionTracking.Models;

namespace ApplicationVersionTracking.Pages.Projects
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationVersionTracking.Data.ApplicationContext _context;

        public DetailsModel(ApplicationVersionTracking.Data.ApplicationContext context)
        {
            _context = context;
        }

        public Project Project { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Project = await _context.Projects.SingleOrDefaultAsync(m => m.ID == id);

            if (Project == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
