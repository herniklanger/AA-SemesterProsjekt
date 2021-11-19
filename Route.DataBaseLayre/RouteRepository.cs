using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using DatabaseLayerCore;
using InterfacesLib;
using Route.DataBaseLayre.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Dapper;

namespace Route.DataBaseLayre
{
    public class RouteRepository : Repository<Models.Route>
    {
        public RouteRepository(IDbConnectionFactory connection) : base(connection)
        {
            if (!Connection.TableExists<Models.Route>())
            {
                Connection.DropAndCreateTable<Customer>();
                Connection.DropAndCreateTable<Vehicle>();
                Connection.DropAndCreateTable<RouteLocations>();
                Connection.DropAndCreateTable<Checkpoint>();
                Connection.DropAndCreateTable<Models.Route>();
            }
        }

        public override async Task<int> CreateAsync(Models.Route entity, CancellationToken cancellationToken = default)
        {
            entity.RouteLocationsList = new List<RouteLocations>();
            foreach (var checkpoint in entity.Checkpoint)
            {
                if (checkpoint.Id != 0)
                {
                    RouteLocations routeLocations = new RouteLocations()
                    {Route = entity, Checkpoint = checkpoint};
                    entity.RouteLocationsList.Add(routeLocations);
                    checkpoint.RouteLocations = entity.RouteLocationsList;
                }
            }
            await Connection.SaveAsync(entity, true, token: cancellationToken);

            return entity.Id;
        }

        public override async Task<Models.Route?> GetAsync(int id, CancellationToken token = default)
        {
            var q = Connection.QueryMultipleAsync(
        "Select \"Checkpoint\".Id, \"Checkpoint\".CustomerId, \"Checkpoint\".LocationX, \"Checkpoint\".LocationY " +
            "FROM \"Checkpoint\" " +
            "JOIN RouteLocations on \"Checkpoint\".Id = RouteLocations.CheckpointId " +
            $"where RouteLocations.RouteId= {id};");
            Models.Route route = await base.GetAsync(id, token);
            route.Checkpoint = (await q).Read<Checkpoint>().ToList();
            foreach (var checkpoint in route.Checkpoint)
            {
                checkpoint.Customers = await Connection.SingleByIdAsync<Customer>(checkpoint.CustomerId);
            }
            return route;
        }
    }
}