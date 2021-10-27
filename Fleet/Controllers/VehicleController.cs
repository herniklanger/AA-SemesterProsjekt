using Fleet.DataBaseLayre;
using InterfacesLib.Fleet;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Fleet.DataBaseLayre.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Fleet.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class VehicleController : ControllerBase
	{
		private readonly FleetRepository _fleetRepository;

		public VehicleController(FleetRepository fleetRepository)
		{
			_fleetRepository = fleetRepository;
		}


		// GET: api/Vehicle
		[HttpGet("ByMake")]
		public async Task<IEnumerable<Vehicle>> GetByMake([FromQuery] string make, CancellationToken cancellationToken = new())
		{
			return await _fleetRepository.GetByMake(make, cancellationToken);
		}

		// GET: api/Vehicle
		[HttpGet]
		public async Task<IEnumerable<Vehicle>> Get(CancellationToken cancellationToken = new())
		{
			return await _fleetRepository.GetAllAsync(cancellationToken);
		}

		// GET api/<Vehicle>/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Vehicle?>> Get(int id, CancellationToken cancellationToken = new())
		{
			var vehicle = await _fleetRepository.GetAsync(id, cancellationToken);
			if (vehicle is null)
			{
				return BadRequest();
			}

			return Ok(vehicle);
		}

		// POST api/<Vehicle>
		[HttpPost]
		public async Task Post([FromBody] Vehicle value, CancellationToken cancellationToken = new())
		{
			await _fleetRepository.CreateAsync(value, cancellationToken);
		}

		// PUT api/<Vehicle>/5
		[HttpPut("{id}")]
		public async Task Put(int id, [FromBody] Vehicle value, CancellationToken cancellationToken = new())
		{
			await _fleetRepository.UpdateAsync(value, cancellationToken);
		}

		// DELETE api/<Vehicle>/5
		[HttpDelete("{id}")]
		public async Task Delete(int id, CancellationToken cancellationToken = new())
		{
			await _fleetRepository.DeleteAsync(id, cancellationToken);
		}
	}
}
