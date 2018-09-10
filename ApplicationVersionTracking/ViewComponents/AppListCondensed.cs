using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationVersionTracking.Models;
using ApplicationVersionTracking.Data;

namespace ApplicationVersionTracking.ViewComponents
{
    public class AppListCondensed : ViewComponent
    {
        private readonly ApplicationContext db;

        public AppListCondensed(ApplicationContext context)
        {
            db = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? topRows)
        {
            var items = await GetItemsAsync(topRows);
            return View(items);
        }
        private Task<List<Application>> GetItemsAsync(int? topRows)
        {
            var res = db.Applications
                .Include(a => a.Events)
                    .ThenInclude(a => a.Server)
                .Include(a => a.Events)
                    .ThenInclude(a => a.EnvironmentType)
                .OrderBy(a => a.Name);

                if (topRows > 0) { return res.Take((int)topRows).ToListAsync(); }

            return res.ToListAsync();
        }
    }
}
