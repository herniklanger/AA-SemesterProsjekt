using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Fleet.DataBaseLayre.Models;
using Fleet.Interfaces;
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
			Connection.DropAndCreateTable<VehicleType>();
			Connection.DropAndCreateTable<Make>();
			Connection.DropAndCreateTable<Model>();
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
			var query = "select * from Vehicle V where V.ModelType == @make";
			return await Connection.QueryAsync<Vehicle>(query, new { make });
		}
	}
}
