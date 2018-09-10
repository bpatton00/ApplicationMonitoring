using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ApplicationVersionTracking.Data;
using ApplicationVersionTracking.Models;

namespace ApplicationVersionTracking.Pages.Servers
{
    public class EditModel : PageModel
    {
        private readonly ApplicationVersionTracking.Data.ApplicationContext _context;

        public EditModel(ApplicationVersionTracking.Data.ApplicationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Server Server { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Server = await _context.Servers
                .Include(a => a.StatusType)
                .SingleOrDefaultAsync(m => m.ID == id);

            if (Server == null)
            {
                return NotFound();
            }
            ViewData["StatusTypeID"] = new SelectList(_context.StatusTypes, "ID", "Description").OrderBy(x => x.Text);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Server).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServerExists(Server.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ServerExists(int id)
        {
            return _context.Servers.Any(e => e.ID == id);
        }
    }
}
