using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ApplicationVersionTracking.Data;
using ApplicationVersionTracking.Models;

namespace ApplicationVersionTracking.Pages.Servers
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
            ViewData["StatusTypeID"] = new SelectList(_context.StatusTypes, "ID", "Description").OrderBy(x => x.Text);
            return Page();
        }

        [BindProperty]
        public Server Server { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Servers.Add(Server);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}