using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Fleet.DataBaseLayre.Models;
using Xunit;
using System;
using System.Data;
using System.Linq;
using InterfacesLib;
using Fleet.DataBaseLayre;
using Fleet.DataBaseLayre.Interfaces;
using MassTransit;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Dapper;

namespace FleetTest.IntergrationTest
{
	public class VehicleControllerTest : FleetSystemContainer
	{
		[Fact]
		public async Task GetAll_Vehicles()
		{
			//Arrange
			//Act
			HttpResponseMessage response = await TestClient.GetAsync("api/Vehicle");
			
			//Assert
			Assert.True(response.IsSuccessStatusCode, await response.Content.ReadAsStringAsync());
			string resultText = await response.Content.ReadAsStringAsync();
			Assert.NotEmpty(resultText);
			List<Vehicle> resultObject = JsonConvert.DeserializeObject<List<Vehicle>>(resultText);
			Assert.Empty(resultObject);
		}


		[Theory]
		[MemberData(nameof(CreateAndGet_VehicleData))]
		public async Task Create_Vehicles(Vehicle vehicle)
		{
			
			var test = JsonConvert.SerializeObject(vehicle);
			//Arrange
			using (var scope = app.Server.Services.CreateScope())
            {
				//Act
				var resoult = await TestClient.PostAsJsonAsync("api/Vehicle", vehicle);
				//Assert
				Assert.True(resoult.IsSuccessStatusCode, await resoult.Content.ReadAsStringAsync());
				string resultText = await resoult.Content.ReadAsStringAsync();
				Assert.NotEmpty(resultText);

				Vehicle resultObject = JsonConvert.DeserializeObject<Vehicle>(resultText);

				IRepository<Vehicle, int> db = scope.ServiceProvider.GetService(typeof(FleetRepository)) as FleetRepository;
				Vehicle expedetResoult = await db.GetAsync(resultObject.Id);
				string expedetText = JsonConvert.SerializeObject(expedetResoult);
				Assert.Equal(expedetText.ToLower(), resultText.ToLower());
            }
		}


		[Theory]
		[MemberData(nameof(CreateAndGet_VehicleData))]
		public async Task Get_Vehicle_By_Id(Vehicle vehicle)
		{
			//Arrange
			int Id;
			using (var scope = app.Services.CreateScope())
            {
				var services = scope.ServiceProvider;
				FleetRepository db = services.GetService(typeof(FleetRepository)) as FleetRepository;
				Id = await db.UpsertAsync(vehicle);
			}
			
			//Act
			HttpResponseMessage response = await TestClient.GetAsync($"api/Vehicle/{Id}");

			//Assert
			Assert.True(response.IsSuccessStatusCode, await response.Content.ReadAsStringAsync());
			
			string resultText = await response.Content.ReadAsStringAsync();
			Assert.NotEmpty(resultText);
			
			string ExpedetText = JsonConvert.SerializeObject(vehicle);
			Assert.Equal(ExpedetText.ToLower(), resultText.ToLower());
		}
		[Theory]
		[MemberData(nameof(CreateAndGet_VehicleData))]
		public async Task Get_Vehicle_By_MakeName(Vehicle vehicle)
		{
			//Arrange
			Make make = vehicle.Make;
			Vehicle[] Resoult = new[] { vehicle };
			using (var scope = app.Services.CreateScope())
            {
				var services = scope.ServiceProvider;
				FleetRepository db = services.GetService(typeof(FleetRepository)) as FleetRepository;
				
				int ObjectId = await db.UpsertAsync(vehicle);
				Resoult[0].Id = ObjectId/*{ await db.GetAsync(ObjectId) }*/;
			}
			vehicle.Make = null;
			vehicle.Model = null;
			vehicle.VehicleType = null;
			//Act
			HttpResponseMessage response = await TestClient.GetAsync($"api/Vehicle/ByMake?make={make.Name}");

			//Assert
			Assert.True(response.IsSuccessStatusCode, await response.Content.ReadAsStringAsync());

			string resultText = await response.Content.ReadAsStringAsync();
			Assert.NotEmpty(resultText);

			string ExpedetText = JsonConvert.SerializeObject(Resoult);
			Assert.Equal(ExpedetText.ToLower(), resultText.ToLower());
		}
		[Theory]
		[MemberData(nameof(CreateAndGet_VehicleData))]
		public async Task Delete_Vehicle_By_Id(Vehicle vehicle)
		{
			//Arrange
			int Id;
			using (var scope = app.Services.CreateScope())
			{
				var services = scope.ServiceProvider;
				services.GetService(typeof(IFleetRepository));
				
				IDbConnection db = (services.GetService(typeof(IDbConnectionFactory)) as IDbConnectionFactory).OpenDbConnection();
				await db.SaveAsync(vehicle, true);//Add 1 vehicle
				int deleateId = vehicle.Id;
				vehicle.Id = 0;
				await db.SaveAsync(vehicle, true);//Add 2 vegicle
				int Before = db.Query<int>("SELECT COUNT(*) FROM Vehicle").FirstOrDefault();
				
				//Act
				HttpResponseMessage response = await TestClient.DeleteAsync($"api/Vehicle/{deleateId}");
				
				//Assert
				Assert.True(response.IsSuccessStatusCode, await response.Content.ReadAsStringAsync());

				int Afer = db.Query<int>("SELECT COUNT(*) FROM Vehicle").FirstOrDefault();
				Assert.Equal(Before - 1, Afer);
			}
		}

		public static IEnumerable<object[]> CreateAndGet_VehicleData()
        {
			DateTime dateTime = DateTime.Now;
			yield return new object[] {
			new Vehicle
			{
				Licenseplate = "CF24542",
				ModelType = "Diesel",
				//RegisteringsDate = dateTime,
				Make = new Make
				{
					Name = "Chevrolet"
				},
				Model = new Model
				{
					Name = "Spark",
					Variant = "1.0"
				},
				VehicleType = new VehicleType
				{
					Name = "Personbil"
				},
				Vinnummer = "1HGBH41JXMN109186"
			}
			};
        }
	}
}
