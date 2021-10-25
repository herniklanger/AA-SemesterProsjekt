using System.Collections.Generic;
using System.Threading.Tasks;
using InterfacesLib;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Dapper;
using Vehicle = Fleet.DataBaseLayre.Models.Vehicle;

namespace Fleet.DataBaseLayre
{
	public class FleetRepository : Repository<Vehicle, int>, IRepository<Vehicle, int>
	{
		public FleetRepository(IDbConnectionFactory connection) : base(connection)
		{
		}

		public async Task<IEnumerable<Vehicle>> GetByMake(string make)
		{
			var query = "select * from Vehicle V where V.ModelType == @make";
			return await Connection.QueryAsync<Vehicle>(query, new { make });
		}
	}
}
