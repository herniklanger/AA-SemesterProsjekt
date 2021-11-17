using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DatabaseLayerCore;
using Driver.DatabaseLayer.Interfaces;
using Driver.DatabaseLayer.Models;
using InterfacesLib;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Dapper;

namespace Driver.DatabaseLayer
{
    public class DriverRepository : Repository<DriverModel>, IDriverRepository
    {
		public DriverRepository(IDbConnectionFactory connection) : base(connection)
		{
            if (!Connection.TableExists<DriverModel>())
            {
                Connection.DropAndCreateTable<Contact>();

                Connection.CreateTable<DriverModel>();
            }
            //Connection.DropAndCreateTable<DriverModel>();
        }

		public override async Task<IEnumerable<DriverModel>> GetAllAsync(CancellationToken token = default)
		{
			var q = Connection.From<DriverModel>()
				.Join<Contact>();
			return await Connection.SelectAsync(q, token);
		}

		public async Task<IEnumerable<DriverModel>> GetByContactType(string contactType, CancellationToken cancellationToken = new())
		{
			var query =
			@"select * 
			from DriverModel 
			JOIN Contact On Contact.DriverId = DriverModel.Id
			WHERE Contact.ContactType = @contactType;";
			Connection.From<DriverModel>()
				.Join<Contact>();
			return await Connection.QueryAsync<DriverModel>(query, new { contactType });
		}
	}
}