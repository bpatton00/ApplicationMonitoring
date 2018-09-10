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
    public class DeleteModel : PageModel
    {
        private readonly ApplicationVersionTracking.Data.ApplicationContext _context;

        public DeleteModel(ApplicationVersionTracking.Data.ApplicationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Application Application { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Application = await _context.Applications
                .Include(a => a.Project)
                .Include(a => a.StatusType).SingleOrDefaultAsync(m => m.ID == id);

            if (Application == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Application = await _context.Applications.FindAsync(id);

            if (Application != null)
            {
                _context.Applications.Remove(Application);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
