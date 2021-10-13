using Businesslogic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Fleet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        // GET: api/Vehicle
        [HttpGet]
        public IEnumerable<Vehicle> Get()
        {
            throw new NotImplementedException();
        }

        // GET api/<Vehicle>/5
        [HttpGet("{id}")]
        public Vehicle Get(int id)
        {
            throw new NotImplementedException();
        }

        // POST api/<Vehicle>
        [HttpPost]
        public void Post([FromBody] Vehicle value)
        {
            throw new NotImplementedException();
        }

        // PUT api/<Vehicle>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Vehicle value)
        {
            throw new NotImplementedException();
        }

        // DELETE api/<Vehicle>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
