using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using DatabaseLayerCore;
using InterfacesLib;
using Route.DataBaseLayre.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;

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
                Connection.CreateTable<Models.Route>();
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
    }
}