using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApplicationVersionTracking.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApplicationVersionTracking.Controllers
{
    [Route("api/[controller]")]
    public class EventController : Controller
    {

        private readonly ApplicationVersionTracking.Data.ApplicationContext _context;
        public EventController(ApplicationVersionTracking.Data.ApplicationContext context)
        {
            _context = context;

            if (_context.Applications.Count() == 0)
            {
                _context.Events.Add(new Event { ApplicationID = 1, EnvironmentTypeID = 1, ServerID = 1, EventTypeID = 1, EventDate = DateTime.Now });
                _context.SaveChanges();
            }
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Event> Get()
        {
            return _context.Events.ToList();
        }

        // GET api/<controller>/5
        [HttpGet("{id}", Name = "GetEvent")]
        public IActionResult GetById(int id)
        {
            var item = _context.Events.FirstOrDefault(t => t.ID == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);            
        }

        // GET api/<controller>/5
        [HttpGet("list/{appname}", Name = "GetEventByApp")]
        public IActionResult GetEventByApp(string appname)
        {
            var item = _context.Events.Where(t => t.Application.Name == appname).OrderByDescending(t => t.EventDate);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        // GET api/<controller>/egrants/Test
        [HttpGet("list/{appname}/{environment}", Name = "GetEventByAppAndEnvironment")]
        public IActionResult GetEventByAppAndEnvironment(string appname, string environment)
        {
            var item = _context.Events.Where(t => t.Application.Name == appname && t.EnvironmentType.Description == environment).OrderByDescending(t => t.EventDate);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        // GET api/<controller>/egrants/Test
        [HttpGet("list/{appname}/{environment}/{release}", Name = "GetSpecificReleaseStatus")]
        public IActionResult GetEventByAppAndEnvironment(string appname, string environment, string release)
        {
            var item = _context.Events.Where(t => t.Application.Name == appname && t.EnvironmentType.Description == environment && t.Version == release).OrderByDescending(t => t.EventDate);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        // GET api/<controller>/5
        [HttpGet("latest/{appname}", Name = "GetInstalledByApp")]
        public IActionResult GetInstalledByApp(string appname, string environment)
        {
            var item = _context.Events.Where(t => t.Application.Name == appname).OrderByDescending(t => t.EventDate).FirstOrDefault(t => t.EnvironmentType.Description == environment);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Create([FromBody] Event item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _context.Events.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetEvent", new { id = item.ID }, item);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Event item)
        {
            //update functionality -- not needed at this time
            return BadRequest();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //delete functionality -- not needed at this time
            return BadRequest();
        }

        [HttpGet("getInstallers/{term}", Name = "getInstallers")]
        public IActionResult getInstallers(string term)
        {            
            return Json(_context.Events.Where(t => t.Installer.Contains(term)).Select(a => new { label = a.Installer }).Distinct());
        }

        [HttpGet("getMSIs/{term}", Name = "getMSIs")]
        public IActionResult getMSIs(string term)
        {
            return Json(_context.Events.Where(t => t.msiFile.Contains(term)).Select(a => new { label = a.msiFile }).Distinct());
        }


        [HttpGet("activity/{period}")]
        public IActionResult BarChart(string period)
        {
            //select events, group by month of event and filter by environment 
            var activityCounts = _context.Events.OrderByDescending(s => s.EventDate).GroupBy(r => new { eMonth = r.EventDate.Month, eYear = r.EventDate.Year })
                    .Select(r => new EventCountModel()
                    {
                        s_eventDate = r.Key.eMonth + "-" + r.Key.eYear,
                        eventCount = r.Count(),
                        developmentCount = r.Where(z => z.EnvironmentType.Description.ToLower() == "development").Count(),
                        testCount = r.Where(z => z.EnvironmentType.Description.ToLower() == "test").Count(),
                        productionCount = r.Where(z => z.EnvironmentType.Description.ToLower() == "production").Count()

                    }).OrderBy(s => Convert.ToDateTime(s.s_eventDate)).Take(20);

            switch (period)
            {
                case "monthly":
                    //default
                    return Json(activityCounts);                    
                case "daily":
                    activityCounts = _context.Events.OrderByDescending(s => s.EventDate).GroupBy(r => r.EventDate.ToShortDateString())
                   .Select(r => new EventCountModel()
                   {
                       s_eventDate = r.Key,
                       eventCount = r.Count()
                   }).OrderByDescending(s => Convert.ToDateTime(s.s_eventDate)).Take(60).OrderBy(s => Convert.ToDateTime(s.s_eventDate));
                    return Json(activityCounts);
            }
            return Json(activityCounts);
            
            /*
        List<EventCountModel> EventCounts = new List<EventCountModel>();
        var results = _context.Events.GroupBy(r => r.EventDate)
                .Select(r => new EventCountModel()
                {
                    eventdate = r.Key,
                    eventcount = r.Count()
                }).ToList();
        foreach(EventCountModel e in results)
        {
            EventCountModel ecm = new EventCountModel();
            ecm.eventdate = e.eventdate;
            ecm.eventcount = e.eventcount;
            EventCounts.Add(ecm);
        }
        return Json(EventCounts);
        */
        }
    }
}
