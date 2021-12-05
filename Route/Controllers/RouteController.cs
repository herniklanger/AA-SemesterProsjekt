using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterfacesLib;
using Newtonsoft.Json;
using Route.BusinesseLayre;
using ServiceStack;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Route.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    public class RouteController : ControllerBase
    {
        private readonly IRepository<DataBaseLayre.Models.Route, int> _repository;
        private readonly RouteCalculatore _routeController = new ();
        public RouteController(IRepository<DataBaseLayre.Models.Route, int> repository)
        {
            _repository = repository;
        }
        
        [HttpGet]
        public async Task<IEnumerable<DataBaseLayre.Models.Route>> Get()
        {
            return await _repository.GetAllAsync();
        }
        [HttpGet("{id}")]
        public async Task<DataBaseLayre.Models.Route> Get(int Id)
        {
            
            DataBaseLayre.Models.Route route = await _repository.GetAsync(Id);
            
            return route;
        }
        
        [HttpPost]
        public async Task<DataBaseLayre.Models.Route> Post([FromBody] DataBaseLayre.Models.Route route)
        {
            route.Id = await _repository.CreateAsync(route);
            return route;
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] DataBaseLayre.Models.Route route)
        {
            var task = _repository.UpdateAsync(route);
            try
            {
                await task;
            }catch(Exception e)
            {
                return StatusCode(304);
            }
            return Ok(route);
        }
        [HttpDelete]
        public async Task<DataBaseLayre.Models.Route> Delete(int Id)
        {
            DataBaseLayre.Models.Route route = await _repository.GetAsync(Id);
            await _repository.DeleteAsync(Id);
            return route;
        }
        
        
        
    }
}
