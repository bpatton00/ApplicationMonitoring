using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationVersionTracking.Models;
using ApplicationVersionTracking.Data;

namespace ApplicationVersionTracking.ViewComponents
{
    public class AppList : ViewComponent
    {
        private readonly ApplicationContext db;

        public AppList(ApplicationContext context)
        {
            db = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await GetItemsAsync();
            return View(items);
        }
        private Task<List<Application>> GetItemsAsync()
        {
            return db.Applications
                .Include(a => a.Events)
                .ToListAsync();
        }
    }
}
