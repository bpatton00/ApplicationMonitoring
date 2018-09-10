using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ApplicationVersionTracking.Data;
using ApplicationVersionTracking.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;

namespace ApplicationVersionTracking.Pages.Events
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationVersionTracking.Data.ApplicationContext _context;

        public CreateModel(ApplicationVersionTracking.Data.ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ApplicationID"] = new SelectList(_context.Applications, "ID", "Name").OrderBy(x => x.Text);
        ViewData["EnvironmentTypeID"] = new SelectList(_context.EnvironmentTypes, "ID", "Description").OrderBy(x => x.Text);
        ViewData["EventTypeID"] = new SelectList(_context.EventTypes, "ID", "Description").OrderBy(x => x.Text);
        ViewData["ServerID"] = new SelectList(_context.Servers, "ID", "Name").OrderBy(x => x.Text);
            return Page();
        }

        [BindProperty]
        public Event Event { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Events.Add(Event);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}