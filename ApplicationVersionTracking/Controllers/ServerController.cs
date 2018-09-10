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
    public class ServerController : Controller
    {
        private readonly ApplicationVersionTracking.Data.ApplicationContext _context;
        public ServerController(ApplicationVersionTracking.Data.ApplicationContext context)
        {
            _context = context;

            if (_context.Applications.Count() == 0)
            {
                _context.Servers.Add(new Server { Name = "Default", StatusTypeID = 1 });
                _context.SaveChanges();
            }
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Server> Get()
        {
            return _context.Servers.ToList();
        }

        //TODO NEED TO BE ABLE TO RETURN ID # based on name or null
        //IF RETURNING NULL< THEN NEED TO ADD THE SERVER
        //OTHER OPTION IS TO AUTOADD AND RETURN THE VALUE HERE

        // GET api/<controller>/5
        [HttpGet("{name}", Name = "GetServer")]
        public IActionResult Get(string name)
        {
            var item = _context.Servers.FirstOrDefault(t => t.Name == name);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);            
        }

        [HttpGet("getServerList/{term}", Name = "getServerList")]
        public IActionResult getInstallers(string term)
        {
            return Json(_context.Servers.Where(t => t.Name.Contains(term)).Select(a => new { label = a.Name }).Distinct());

        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Create([FromBody] Server item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _context.Servers.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetServer", new { name = item.Name }, item);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
