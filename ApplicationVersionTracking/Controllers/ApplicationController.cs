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
    public class ApplicationController : Controller
    {
        private readonly ApplicationVersionTracking.Data.ApplicationContext _context;
        public ApplicationController(ApplicationVersionTracking.Data.ApplicationContext context)
        {
            _context = context;

            if (_context.Applications.Count() == 0)
            {
                _context.Applications.Add(new Application { Name = "Default", ProjectID = 1, StatusTypeID = 1, ActiveTFRelease = "rel0000" });
                _context.SaveChanges();
            }
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Application> Get()
        {
            return _context.Applications.ToList();
        }

        // GET api/<controller>/5
        [HttpGet("{name}", Name = "GetApplication")]
        public IActionResult Get(string name)
        {
            var item = _context.Applications.FirstOrDefault(t => t.Name == name);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }        

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]Application item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _context.Applications.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetApplication", new { name = item.Name }, item);
        }
        
        //update functionality
        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public string Put(int id, [FromBody]Application item)
        {
            if (item == null)
            {
                return "Bad Request";
            }

            _context.Applications.Update(item);
            _context.SaveChanges();

            return "Item Updated";
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
