using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fleet.DataBaseLayre.Interfaces;
using Fleet.DataBaseLayre.Models;
using InterfacesLib;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Dapper;

namespace Fleet.DataBaseLayre
{
	public class FleetRepository : Repository<Vehicle>, IFleetRepository
	{
		public FleetRepository(IDbConnectionFactory connection) : base(connection)
		{
            //Connection.DropTable<Vehicle>();
            //Connection.DropAndCreateTable<Make>();
            //Connection.DropAndCreateTable<Model>();
            //Connection.DropAndCreateTable<VehicleType>();
            if (!Connection.TableExists<Vehicle>())
            {
				Connection.DropAndCreateTable<Make>();
				Connection.DropAndCreateTable<Model>();
				Connection.DropAndCreateTable<VehicleType>();
				Connection.CreateTable<Vehicle>();
            }
		}

		public override async Task<Vehicle?> GetAsync(int id, CancellationToken token = default)
		{
			var q = Connection.From<Vehicle>()
				.Join<Make>()
				.Join<Model>()
				.Join<VehicleType>()
				.Where(vehicle => vehicle.Id == id);
			return (await Connection.SelectAsync(q, token)).FirstOrDefault();
		}

		public override async Task<IEnumerable<Vehicle>> GetAllAsync(CancellationToken token = default)
		{
			var q = Connection.From<Vehicle>()
				.Join<Make>()
				.Join<Model>()
				.Join<VehicleType>();
			return await Connection.SelectAsync(q, token);
		}

		public async Task<IEnumerable<Vehicle>> GetByMake(string make, CancellationToken cancellationToken = new())
		{
			var query = 
			@"select * 
			from Vehicle 
			JOIN Make On Make.Id = Vehicle.MakeId
			WHERE Make.Name = @make;";
			Connection.From<Vehicle>()
				.Join<Make>()
				.Join<Model>()
				.Join<VehicleType>();
			return await Connection.QueryAsync<Vehicle>(query, new { make });
		}
	}
}
