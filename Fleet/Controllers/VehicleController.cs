using Fleet.DataBaseLayre;
using InterfacesLib.Fleet;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Fleet.DataBaseLayre.Interfaces;
using Fleet.DataBaseLayre.Models;
using DatabaseLayerCore.MessageBus.Models;
using InterfacesLib;
using MassTransit;
using ServiceStack.Messaging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Fleet.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class VehicleController : ControllerBase
	{
		private readonly IRepository<Vehicle, int> _repository;
		private readonly IFleetRepository _fleetRepository;
		private readonly IBus _messageBus;
		public VehicleController(IRepository<Vehicle,int> repository, IFleetRepository fleetRepository, IBus messageBus)
		{
			_fleetRepository = fleetRepository;
			_repository = repository;
			_messageBus = messageBus;
		}


		// GET: api/Vehicle/ByMake
		[HttpGet("ByMake")]
		public async Task<IEnumerable<Vehicle>> GetByMake([FromQuery] string make, CancellationToken cancellationToken = new())
		{
			IEnumerable<Vehicle> value = await _fleetRepository.GetByMake(make, cancellationToken);
			return value;
		}

		// GET: api/Vehicle
		[HttpGet]
		public async Task<IEnumerable<Vehicle>> Get(CancellationToken cancellationToken = new())
		{
			return await _repository.GetAllAsync(cancellationToken);
		}

		// GET api/Vehicle/<id>
		[HttpGet("{id}")]
		public async Task<ActionResult<Vehicle?>> Get(int id, CancellationToken cancellationToken = new())
		{
			var vehicle = await _repository.GetAsync(id, cancellationToken);
			if (vehicle is null)
			{
				return BadRequest();
			}

			return Ok(vehicle);
		}

		// POST api/Vehicle
		[HttpPost]
		public async Task<ActionResult<Vehicle>> Post([FromBody] Vehicle value, CancellationToken cancellationToken = new())
		{
			var id = await _repository.UpsertAsync(value, cancellationToken);
			var vehicle = await _repository.GetAsync(id, cancellationToken);
			
			await _messageBus.Publish(new UpdateMessage<Vehicle>{ Message = vehicle });
			return vehicle is not null 
				? Ok(vehicle) 
				: StatusCode(500);
		}

		// PUT api/Vehicle/<id>
		[HttpPut("{id}")]
		public async Task<ActionResult<Vehicle>> Put(int id, [FromBody] Vehicle value, CancellationToken cancellationToken = new())
		{
			value.Id = id;
			var updatedId = await _repository.UpdateAsync(value, cancellationToken);
			var vehicle = await _repository.GetAsync(updatedId, cancellationToken);
			await _messageBus.Publish(new UpdateMessage<Vehicle>{ Message = vehicle });

			return vehicle is not null
				? Ok(vehicle)
				: StatusCode(500);
		}

		// DELETE api/<Vehicle>/5
		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken = new())
		{
			await _repository.DeleteAsync(id, cancellationToken);
			await _messageBus.Publish(new DeleateMessage<Vehicle>{ Message = new Vehicle{ Id = id } });
			return Ok();
		}
	}
}
