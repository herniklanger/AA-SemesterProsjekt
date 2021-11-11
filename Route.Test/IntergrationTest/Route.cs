using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using InterfacesLib;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Route.DataBaseLayre.Models;

namespace Route.Test.IntergrationTest
{
    public class RouteControllerTest : RouteSystemContainer
    {
        public void CreateRoute()
        {
            //Arrange
            //Creadet a new route   
            
            //Act
            
            //Assert
            
        }
        // [Theory]
        // [MethodImpl( MethodImplOptions.NoInlining)]
        public async void GetRoute(DataBaseLayre.Models.Route InputRoute)
        {
            //Arrange
            using(var scope = app.Services.CreateScope())
            {
                
                IServiceProvider services = scope.ServiceProvider;
                IRepository<DataBaseLayre.Models.Route, int> context = services.GetRequiredService<IRepository<DataBaseLayre.Models.Route, int>>();
            }
            
            //Act client
            HttpResponseMessage response = await TestClient.GetAsync("/api/Route/");

            //Assert
            
        }

        public IEnumerable<Object[]> CreateTestRoutes()
        {
            //Create a new Route
            DataBaseLayre.Models.Route route = new ()
            {
                Id = 0,
                Name = "TestRoute",
                StartCheckpoint = new Location()
                {
                    X = 1.2321M,
                    Y = 2.1234M
                },
                EndCheckpoint = new Location()
                {
                    X = 2.2341M,
                    Y = 10.2235M
                },
                Vehicle = new Vehicle()
                {
                    Id = 1
                },
                RouteLocationsList = new List<RouteLocations>()
                
            };
            //Create a new Customer
            Customer customer = new Customer()
            {
                Id = 1,
                Name = "Test",
                Locations = new List<Checkpoint>()
            };
            //Create a new CustomerLocations
            Checkpoint customerLocation = new ()
            {
                Id = 0,
                Location = new Location()
                {
                    X = 1.2321M,
                    Y = 2.1234M
                },
                RouteLocations = new List<RouteLocations>(),
                Customers = customer
            };
            //Create a new RouteLocations
            RouteLocations routeLocations = new ()
            {
                Id = 0,
                Route = route,
                Checkpoint = customerLocation
            };
            //Add References
            route.RouteLocationsList.Add(routeLocations);
            customerLocation.RouteLocations.Add(routeLocations);
            customer.Locations.Add(customerLocation);
            yield return new []
            {
                route
            };
        }
    }
}
