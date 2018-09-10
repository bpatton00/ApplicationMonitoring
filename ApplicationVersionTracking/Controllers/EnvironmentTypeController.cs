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
    public class EnvironmentTypeController : Controller
    {
        private readonly ApplicationVersionTracking.Data.ApplicationContext _context;
        public EnvironmentTypeController(ApplicationVersionTracking.Data.ApplicationContext context)
        {
            _context = context;

            if (_context.Applications.Count() == 0)
            {
                _context.EnvironmentTypes.Add(new EnvironmentType { Description = "Default" });
                _context.SaveChanges();
            }
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<EnvironmentType> Get()
        {
            return _context.EnvironmentTypes.ToList();
        }

        // GET api/<controller>/5
        [HttpGet("{description}", Name = "GetEvironmentType")]
        public IActionResult Get(string description)
        {
            var item = _context.EnvironmentTypes.FirstOrDefault(t => t.Description == description);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
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
