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

namespace ApplicationVersionTracking.Pages.Events
{
    public class EditModel : PageModel
    {
        private readonly ApplicationVersionTracking.Data.ApplicationContext _context;

        public EditModel(ApplicationVersionTracking.Data.ApplicationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Event Event { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Event = await _context.Events
                .Include(a => a.Application)
                .Include(a => a.EnvironmentType)
                .Include(a => a.EventType)
                .Include(a => a.Server).SingleOrDefaultAsync(m => m.ID == id);

            if (Event == null)
            {
                return NotFound();
            }
           ViewData["ApplicationID"] = new SelectList(_context.Applications, "ID", "Name").OrderBy(x => x.Text);
           ViewData["EnvironmentTypeID"] = new SelectList(_context.EnvironmentTypes, "ID", "Description").OrderBy(x => x.Text);
           ViewData["EventTypeID"] = new SelectList(_context.EventTypes, "ID", "Description").OrderBy(x => x.Text);
           ViewData["ServerID"] = new SelectList(_context.Servers, "ID", "Name").OrderBy(x => x.Text);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Event).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(Event.ID))
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

        private bool EventExists(long id)
        {
            return _context.Events.Any(e => e.ID == id);
        }
    }
}
