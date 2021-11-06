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
                Connection.DropAndCreateTable<Location>();
                Connection.DropAndCreateTable<RouteLocations>();
                Connection.CreateTable<Models.Route>();
            }
        }
    }
}