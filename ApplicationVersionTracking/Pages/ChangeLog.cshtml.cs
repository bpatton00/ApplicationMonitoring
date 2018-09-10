using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ApplicationVersionTracking.Pages
{
    public class ChangeLogModel : PageModel
    {
        public string Message { get; set; }

        public void OnGet()
        {
            Message = "List of revisions in order.";
        }
    }
}