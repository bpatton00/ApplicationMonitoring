using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using ApplicationVersionTracking.Models;
using ApplicationVersionTracking.Data;

namespace ApplicationVersionTracking.ViewComponents
{
    public class DeploymentStatistics : ViewComponent
    {

        private readonly ApplicationContext db;

        public DeploymentStatistics(ApplicationContext context)
        {
            db = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await GetItemsAsync();
            return View(items);
        }
        private Task<List<Event>> GetItemsAsync()
        {
            return db.Events
                .Include(a => a.EnvironmentType)
                .Include(a => a.EventType)
                .ToListAsync();
        }

    }
}
