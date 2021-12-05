using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using InterfacesLib;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Route.DataBaseLayre;
using Xunit;
using Route.DataBaseLayre.Models;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Dapper;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Route.Test.IntergrationTest
{
    public class RouteControllerTest : RouteSystemContainer
    {
        [Theory]
        [MemberData(nameof(CreateTestRoutes))]
        public async void GetAll_Route(DataBaseLayre.Models.Route InputRoute)
        {
            //Arrange
            using (var scope = app.Services.CreateScope())
            {
                IServiceProvider services = scope.ServiceProvider;
                IRepository<DataBaseLayre.Models.Route, int> context =
                    services.GetRequiredService<IRepository<DataBaseLayre.Models.Route, int>>();
                InputRoute.Id = await context.CreateAsync(InputRoute);
                //Act
                HttpResponseMessage response = await TestClient.GetAsync("/api/Route");

                //Assert
                string responseString = await response.Content.ReadAsStringAsync();
                response.IsSuccessStatusCode.Should().BeTrue(responseString);

                string resultText = await response.Content.ReadAsStringAsync();
                resultText.Should().NotBeNullOrEmpty(resultText);


                List<DataBaseLayre.Models.Route> result =
                    JsonConvert.DeserializeObject<List<DataBaseLayre.Models.Route>>(resultText);
                IEnumerable<DataBaseLayre.Models.Route> ExpedetRoutes = await context.GetAllAsync();
                foreach (DataBaseLayre.Models.Route ExpedetRoute in ExpedetRoutes)
                {
                    result.Should().ContainEquivalentOf(ExpedetRoute, x => 
                        x.ExcludingNestedObjects()
                            .Excluding(y=>y.VehicleId));
                }
            }
        }

        [Theory]
        [MemberData(nameof(CreateTestRoutes))]
        public async void GetById_Route(DataBaseLayre.Models.Route InputRoute)
        {
            InputRoute.StartCheckpointX = 213.23M;
            //Arrange
            using (var scope = app.Services.CreateScope())
            {

                IServiceProvider services = scope.ServiceProvider;
                IRepository<DataBaseLayre.Models.Route, int> context =
                    services.GetRequiredService<IRepository<DataBaseLayre.Models.Route, int>>();
                IDbConnection db = scope.ServiceProvider.GetService<IDbConnectionFactory>().OpenDbConnection();

                await db.SaveAllAsync(InputRoute.Checkpoint.ConvertAll<Customer>(x => x.Customer));
                await db.SaveAllAsync(InputRoute.Checkpoint);
                await db.SaveAsync(InputRoute.Vehicle);
                InputRoute.RouteLocationsList = new List<RouteLocations>();
                InputRoute.RouteLocationsList = InputRoute.Checkpoint.ConvertAll(x => new RouteLocations()
                    { Checkpoint = x });
                InputRoute.Id = await context.CreateAsync(InputRoute);
            }

            //Act client
            HttpResponseMessage response = await TestClient.GetAsync($"/api/Route/{InputRoute.Id}");

            //Assert
            string responseString = await response.Content.ReadAsStringAsync();
            response.IsSuccessStatusCode.Should().BeTrue(responseString);
            
            string resultText = await response.Content.ReadAsStringAsync();
            resultText.Should().NotBeNullOrEmpty();


            DataBaseLayre.Models.Route result = JsonConvert.DeserializeObject<DataBaseLayre.Models.Route>(resultText);
            result.Should().BeEquivalentTo(InputRoute, x =>
            {
                x.Excluding(y => y.RouteLocationsList).Excluding(y => y.VehicleId)
                    .Excluding(y => y.RouteLocationsList)
                    .Excluding(y => y.Checkpoint);
                return x;
            });
        }


        [Theory]
        [MemberData(nameof(CreateTestRoutes))]
        public async void Create_Route(DataBaseLayre.Models.Route InputRoute)
        {
            //Arrange
            using (var scope = app.Services.CreateScope())
            {
                scope.ServiceProvider.GetService<RouteRepository>();
                IDbConnection db = scope.ServiceProvider.GetService<IDbConnectionFactory>().OpenDbConnection();

                await db.SaveAllAsync(InputRoute.Checkpoint.ConvertAll<Customer>(x => x.Customer));
                await db.SaveAllAsync(InputRoute.Checkpoint);
                await db.SaveAsync(InputRoute.Vehicle);
                
                //Act
                HttpResponseMessage response = await TestClient.PostAsJsonAsync("/api/Route/", InputRoute);

                //Assert
                response.IsSuccessStatusCode.Should().BeTrue(response.Content.ReadAsStringAsync().Result);
                
                string resoultText = await response.Content.ReadAsStringAsync();
                resoultText.Should().NotBeNullOrEmpty();

                DataBaseLayre.Models.Route resoultObject =
                    JsonConvert.DeserializeObject<DataBaseLayre.Models.Route>(resoultText);
                
                DataBaseLayre.Models.Route routeDbStoreage =
                    await db.LoadSingleByIdAsync<DataBaseLayre.Models.Route>(resoultObject.Id);
                
                
                routeDbStoreage.Should().BeEquivalentTo(InputRoute, x =>
                {
                    x.Excluding(y => y.Id);
                    x.Excluding(y => y.Checkpoint); 
                    x.Excluding(y => y.VehicleId);
                    x.Excluding(y => y.RouteLocationsList);
                    return x;
                });
            }

        }

        [Theory]
        [MemberData(nameof(CreateTestRoutes))]
        //TODO: Quesetion Du we move RouteLocations update to it own request
        //TODO: Question What happens if Checkpoint dossent exit
        //TODO: Question What happens if only the new data is pobuladet
        public async void Update_Route(DataBaseLayre.Models.Route resoultRoute)
        {
            //Arrange
            using (var scope = app.Services.CreateScope())
            {
                resoultRoute.Checkpoint.ForEach(x => x.CustomerId = x.Customer.Id);
                Checkpoint checkpoint = new Checkpoint(){Customer = resoultRoute.Checkpoint[0].Customer, Location = new Location(){X = 23.123M, Y = 20.213M},CustomerId = resoultRoute.Checkpoint[0].Customer.Id};
                IServiceProvider services = scope.ServiceProvider;
                IDbConnection db = services.GetService<IDbConnectionFactory>().OpenDbConnection();
                IRepository<DataBaseLayre.Models.Route, int> context =
                    services.GetRequiredService<IRepository<DataBaseLayre.Models.Route, int>>();
                await db.SaveAllAsync(resoultRoute.Checkpoint.ConvertAll<Customer>(x => x.Customer));
                await db.SaveAllAsync(resoultRoute.Checkpoint);
                await db.SaveAsync(checkpoint, true);
                await db.SaveAsync(resoultRoute.Vehicle, true);
                await db.SaveAsync(resoultRoute, true);
                resoultRoute.Checkpoint.Add(checkpoint);
            //Act
            HttpResponseMessage response = await TestClient.PutAsJsonAsync("/api/Route/", resoultRoute);

            //Assert
            string resoultText = await response.Content.ReadAsStringAsync();
            response.IsSuccessStatusCode.Should().BeTrue(resoultText);
            resoultText.Should().NotBeNullOrEmpty();


            DataBaseLayre.Models.Route resoultObject =
                JsonConvert.DeserializeObject<DataBaseLayre.Models.Route>(resoultText);
           
                DataBaseLayre.Models.Route routeDbStoreage =
                    await db.LoadSingleByIdAsync<DataBaseLayre.Models.Route>(resoultObject.Id);
                routeDbStoreage.Checkpoint = new List<Checkpoint>();
                foreach (RouteLocations routeLocations in routeDbStoreage.RouteLocationsList)
                {
                    Checkpoint chek = await db.LoadSingleByIdAsync<Checkpoint>(routeLocations.CheckpointId);
                    chek.Customer = db.Select<Customer>(x=>x.Id == chek.CustomerId).FirstOrDefault();
                    routeDbStoreage.Checkpoint.Add(chek);
                }

                routeDbStoreage.Should().BeEquivalentTo(resoultRoute, x =>
                {
                    x.Excluding(y => y.RouteLocationsList);
                    x.Excluding(y => y.VehicleId);
                    x.Excluding(y => y.Checkpoint);
                    return x;
                });
                routeDbStoreage.Checkpoint.Should()
                    .BeEquivalentTo(resoultRoute.Checkpoint, x => 
                        x.Excluding(y => y.RouteLocations)
                            .Excluding(y => y.Customer.Locations));
            }
        }

        [Theory]
        [MemberData(nameof(CreateTestRoutes))]
        public async void Deleate_Route(DataBaseLayre.Models.Route InputRoute)
        {
            //Arrange
            using (var scope = app.Services.CreateScope())
            {
                IServiceProvider services = scope.ServiceProvider;
                IDbConnection db = services.GetService<IDbConnectionFactory>().OpenDbConnection();
                IRepository<DataBaseLayre.Models.Route, int> context =
                    services.GetRequiredService<IRepository<DataBaseLayre.Models.Route, int>>();
                await db.SaveAllAsync(InputRoute.Checkpoint.ConvertAll<Customer>(x => x.Customer));
                await db.SaveAllAsync(InputRoute.Checkpoint);
                await db.SaveAsync(InputRoute.Vehicle);
                await db.SaveAsync(InputRoute);

                
                int Before = db.Query<int>("SELECT COUNT(*) FROM Route").FirstOrDefault();

                //Act
                HttpResponseMessage response = await TestClient.DeleteAsync($"/api/Route?Id={InputRoute.Id}");

                //Assert
                response.IsSuccessStatusCode.Should().BeTrue(response.Content.ReadAsStringAsync().Result);
                int Afer = db.Query<int>("SELECT COUNT(*) FROM Route").FirstOrDefault();
                Afer.Should().Be(Before - 1);
            }
        }
        public static IEnumerable<Object[]> CreateTestRoutes()
        {
            //Create a new Route
            yield return new[]
            {
                new DataBaseLayre.Models.Route()
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
                    Checkpoint = new()
                    {
                        new()
                        {
                            Id = 0,
                            Location = new Location()
                            {
                                X = 1.2321M,
                                Y = 2.1234M
                            },
                            RouteLocations = new List<RouteLocations>(),
                            Customer = new Customer()
                            {
                                Id = 1,
                                Name = "Test",
                                Locations = new List<Checkpoint>()
                            }
                        }
                    }
                }
            };
        }
    }
}
