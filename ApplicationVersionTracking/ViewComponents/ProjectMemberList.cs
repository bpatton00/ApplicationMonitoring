using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationVersionTracking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApplicationVersionTracking.Data;

namespace ApplicationVersionTracking.ViewComponents
{
    public class ProjectMemberList : ViewComponent
    {
        private readonly ApplicationContext db;

        public ProjectMemberList(ApplicationContext context)
        {
            db = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(long? projectID)
        {
            var items = await GetItemsAsync(projectID);
            return View(items);
        }
        private Task<List<Application>> GetItemsAsync(long? projectID)
        {
            return db.Applications
                .Where(a => a.ProjectID == projectID)
                .Include(a => a.Events)
                    .ThenInclude(a => a.Server)
                        .ThenInclude(a => a.StatusType)
                .Include(a => a.Project)
                .Include(a => a.StatusType)
                .ToListAsync();
        }


    }
}
