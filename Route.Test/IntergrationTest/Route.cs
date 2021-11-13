using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using FluentAssertions;
using InterfacesLib;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;
using Route.DataBaseLayre.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Dapper;

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
                HttpResponseMessage response = await TestClient.GetAsync("/api/Route/");

                //Assert
                response.IsSuccessStatusCode.Should().BeTrue();

                string resultText = await response.Content.ReadAsStringAsync();
                resultText.Should().NotBeNullOrEmpty();


                List<DataBaseLayre.Models.Route> result =
                    JsonConvert.DeserializeObject<List<DataBaseLayre.Models.Route>>(resultText);
                IEnumerable<DataBaseLayre.Models.Route> ExpedetRoutes = await context.GetAllAsync();
                foreach (DataBaseLayre.Models.Route ExpedetRoute in ExpedetRoutes)
                {
                    result.Should().ContainEquivalentOf(ExpedetRoute, x => x.ExcludingNestedObjects());
                }
            }


        }

        [Theory]
        [MemberData(nameof(CreateTestRoutes))]
        public async void GetById_Route(DataBaseLayre.Models.Route InputRoute)
        {
            //Arrange
            using (var scope = app.Services.CreateScope())
            {

                IServiceProvider services = scope.ServiceProvider;
                IRepository<DataBaseLayre.Models.Route, int> context =
                    services.GetRequiredService<IRepository<DataBaseLayre.Models.Route, int>>();
                InputRoute.Id = await context.CreateAsync(InputRoute);
            }

            //Act client
            HttpResponseMessage response = await TestClient.GetAsync("/api/Route?Id=InputRoute.Id");

            //Assert
            response.IsSuccessStatusCode.Should().BeTrue();

            string resultText = await response.Content.ReadAsStringAsync();
            resultText.Should().NotBeNullOrEmpty();


            DataBaseLayre.Models.Route result = JsonConvert.DeserializeObject<DataBaseLayre.Models.Route>(resultText);
            result.Should().BeEquivalentTo(InputRoute, x => x.Excluding(y => y.RouteLocationsList).Excluding(y=>y.VehicleId));
        }


        [Theory]
        [MemberData(nameof(CreateTestRoutes))]
        public async void Create_Route(DataBaseLayre.Models.Route InputRoute)
        {
            //Arrange

            //Act
            HttpResponseMessage response = await TestClient.PostAsJsonAsync("/api/Route/", InputRoute);

            //Assert
            string resoultText = await response.Content.ReadAsStringAsync();
            resoultText.Should().NotBeNullOrEmpty();

            DataBaseLayre.Models.Route resoultObject =
                JsonConvert.DeserializeObject<DataBaseLayre.Models.Route>(resoultText);
            using (var scope = app.Services.CreateScope())
            {
                IDbConnection db = scope.ServiceProvider.GetService<IDbConnectionFactory>().OpenDbConnection();
                DataBaseLayre.Models.Route routeDbStoreage =
                    await db.LoadSingleByIdAsync<DataBaseLayre.Models.Route>(resoultObject.Id);
                routeDbStoreage.Checkpoint = new List<Checkpoint>();
                foreach (RouteLocations routeLocations in routeDbStoreage.RouteLocationsList)
                {
                    routeDbStoreage.Checkpoint.Add(
                        await db.LoadSingleByIdAsync<Checkpoint>(routeLocations.CheckpointId));
                }

                routeDbStoreage.Should().BeSameAs(InputRoute);
            }

        }

        [Theory]
        [MemberData(nameof(CreateTestRoutes))]
        public async void Update_Route(DataBaseLayre.Models.Route InputRoute)
        {
            //Arrange

            //Act
            HttpResponseMessage response = await TestClient.PostAsJsonAsync("/api/Route/", InputRoute);

            //Assert
            string resoultText = await response.Content.ReadAsStringAsync();
            resoultText.Should().NotBeNullOrEmpty();

            DataBaseLayre.Models.Route resoultObject =
                JsonConvert.DeserializeObject<DataBaseLayre.Models.Route>(resoultText);
            using (var scope = app.Services.CreateScope())
            {
                IDbConnection db = scope.ServiceProvider.GetService<IDbConnectionFactory>().OpenDbConnection();
                DataBaseLayre.Models.Route routeDbStoreage =
                    await db.LoadSingleByIdAsync<DataBaseLayre.Models.Route>(resoultObject.Id);
                routeDbStoreage.Checkpoint = new List<Checkpoint>();
                foreach (RouteLocations routeLocations in routeDbStoreage.RouteLocationsList)
                {
                    routeDbStoreage.Checkpoint.Add(
                        await db.LoadSingleByIdAsync<Checkpoint>(routeLocations.CheckpointId));
                }

                routeDbStoreage.Should().BeSameAs(InputRoute);
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
                int Before = db.Query<int>("SELECT COUNT(*) FROM Vehicle").FirstOrDefault();

                //Act
                HttpResponseMessage response = await TestClient.GetAsync("/api/Route/");

                //Assert
                int Afer = db.Query<int>("SELECT COUNT(*) FROM Vehicle").FirstOrDefault();
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
                            Customers = new Customer()
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
