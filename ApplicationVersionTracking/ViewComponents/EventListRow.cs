using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationVersionTracking.Models;
using ApplicationVersionTracking.Data;

namespace ApplicationVersionTracking.ViewComponents
{
    public class EventListRow : ViewComponent
    {
        private readonly ApplicationContext db;

        public EventListRow(ApplicationContext context)
        {
            db = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? applicationID, int? environmentID)
        {
            var items = await GetItemsAsync(applicationID, environmentID);
            return View(items);
        }
        private Task<List<Event>> GetItemsAsync(int? applicationID, int? environmentID)
        {
            if (applicationID != null && environmentID != null)
            {
                return db.Events.Where(x => x.ApplicationID == applicationID &&
                x.EnvironmentTypeID == environmentID)
                    .Include(a => a.Application)
                    .Include(a => a.Server)
                    .OrderByDescending(a => a.EventDate)
                    .ToListAsync();
            }
            else if (applicationID != null && environmentID == null)
            {
                return db.Events.Where(x => x.ApplicationID == applicationID)
                    .Include(a => a.Application)
                    .Include(a => a.Server)
                    .OrderByDescending(a => a.EventDate)
                    .ToListAsync();
            }
            else if (applicationID == null && environmentID != null)
            {
                return db.Events.Where(x => 
                x.EnvironmentTypeID == environmentID)
                    .Include(a => a.Application)
                    .Include(a => a.Server)
                    .OrderByDescending(a => a.EventDate)
                    .ToListAsync();
            }

                return null;
        }
    }
}
