using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationVersionTracking.Models;
using ApplicationVersionTracking.Data;

namespace ApplicationVersionTracking.ViewComponents
{
    public class EventList : ViewComponent
    {
        private readonly ApplicationContext db;

        public EventList(ApplicationContext context)
        {
            db = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? applicationID, int? serverID, int? environmentID, int? topRows)
        {
            var items = await GetItemsAsync(applicationID, serverID, environmentID, topRows);
            return View(items);
        }
        private Task<List<Event>> GetItemsAsync(int? applicationID, int? serverID, int? environmentID, int? topRows)
        {
            // Inventory by Server
            if (serverID != null)
            {
                ViewData["serverID"] = serverID;
                var res = db.Events.Where(x => x.ServerID == serverID)
                       .Include(a => a.Application)
                       .Include(a => a.Server)
                       .OrderByDescending(a => a.EventDate);

                //if (topRows > 0) { return res.Take((int)topRows).ToListAsync(); }
                return res.ToListAsync();
            }

            //application event history
            if (applicationID != null && environmentID != null)
            {
                var res = db.Events.Where(x => x.ApplicationID == applicationID &&
                   x.EnvironmentTypeID == environmentID)
                       .Include(a => a.Application)
                       .Include(a => a.Server).OrderByDescending(a => a.EventDate);

                //if (topRows > 0) { return res.Take((int)topRows).ToListAsync(); }
                return res.ToListAsync();
            }
            else if (applicationID != null && environmentID == null)
            {
                var res = db.Events.Where(x => x.ApplicationID == applicationID)
                    .Include(a => a.Application)
                    .Include(a => a.Server).OrderByDescending(a => a.EventDate);

                //if (topRows > 0) { return res.Take((int)topRows).ToListAsync(); }
                return res.ToListAsync();                    
            }
            else if (applicationID == null && environmentID != null)
            {
                var res = db.Events.Where(x =>
                x.EnvironmentTypeID == environmentID)
                    .Include(a => a.Application)
                    .Include(a => a.Server).OrderByDescending(a => a.EventDate);

                //if (topRows > 0) { return res.Take((int)topRows).ToListAsync(); }
                return res.ToListAsync();                    
            }

                return null;
        }
    }
}
