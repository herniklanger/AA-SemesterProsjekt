using Driver.DatabaseLayer;
using InterfacesLib.Fleet;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Driver.DatabaseLayer.Interfaces;
using Driver.DatabaseLayer.Models;
using InterfacesLib;


namespace Driver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private readonly IRepository<DriverModel, int> _repository;
        private readonly IDriverRepository _driverRepository;

        public DriverController(IRepository<DriverModel, int> repository, IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
            _repository = repository;
        }

        // GET: api/<DriverController>
        [HttpGet("ContactType")]
		public async Task<IEnumerable<DriverModel>> GetByMake([FromQuery] string contactType, CancellationToken cancellationToken = new())
		{
			IEnumerable<DriverModel> value = await _driverRepository.GetByContactType(contactType, cancellationToken);
			return value;
		}

		// GET: api/Driver
		[HttpGet]
		public async Task<IEnumerable<DriverModel>> Get(CancellationToken cancellationToken = new())
		{
			return await _repository.GetAllAsync(cancellationToken);
		}

		// GET api/Driver/<id>
		[HttpGet("{id}")]
		public async Task<ActionResult<DriverModel?>> Get(int id, CancellationToken cancellationToken = new())
		{
			var driver = await _repository.GetAsync(id, cancellationToken);
			if (driver is null)
			{
				return BadRequest();
			}

			return Ok(driver);
		}

		// POST api/Driver
		[HttpPost]
		public async Task<ActionResult<DriverModel>> Post([FromBody] DriverModel value, CancellationToken cancellationToken = new())
		{
			var id = await _repository.UpsertAsync(value, cancellationToken);
			var driver = await _repository.GetAsync(id, cancellationToken);

			return driver is not null
				? Ok(driver)
				: StatusCode(500);
		}

		// PUT api/Driver/<id>
		[HttpPut("{id}")]
		public async Task<ActionResult<DriverModel>> Put(int id, [FromBody] DriverModel value, CancellationToken cancellationToken = new())
		{
			value.Id = id;
			var updatedId = await _repository.UpdateAsync(value, cancellationToken);
			var driver = await _repository.GetAsync(updatedId, cancellationToken);

			return driver is not null
				? Ok(driver)
				: StatusCode(500);
		}

		// DELETE api/<Driver>/5
		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken = new())
		{
			await _repository.DeleteAsync(id, cancellationToken);
			return Ok();
		}       
    }
}
