using System.Text.Json;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Fleet.DataBaseLayre.Models;
using Xunit;
using System;
using InterfacesLib;
using Fleet.DataBaseLayre;
using Microsoft.Extensions.DependencyInjection;

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
			List<Vehicle> resultObject = System.Text.Json.JsonSerializer.Deserialize<List<Vehicle>>(resultText);
			Assert.Empty(resultObject);
		}


		[Theory]
		[MemberData(nameof(CreateAndGet_VehicleData))]
		public async Task CreateAll_Vehicles(Vehicle vehicle)
		{
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
				string expedetText = System.Text.Json.JsonSerializer.Serialize(expedetResoult);
				Assert.Equal(expedetText.ToLower(), resultText.ToLower());
            }
		}


		[Theory]
		[MemberData(nameof(CreateAndGet_VehicleData))]
		public async Task Get_Vehicle(Vehicle vehicle)
		{
			//Arrange
			int test;
			Vehicle Resoult = null;
			using (var scope = app.Services.CreateScope())
            {
				var services = scope.ServiceProvider;
				FleetRepository db = services.GetService(typeof(FleetRepository)) as FleetRepository;
				test = await db.UpsertAsync(vehicle);
				Resoult = await db.GetAsync(test);
			}
			Resoult.Id = test;
			//Act
			HttpResponseMessage response = await TestClient.GetAsync($"api/Vehicle/{test}");

			//Assert
			Assert.True(response.IsSuccessStatusCode, await response.Content.ReadAsStringAsync());

			string resultText = await response.Content.ReadAsStringAsync();
			Assert.NotEmpty(resultText);

			string ExpedetText = System.Text.Json.JsonSerializer.Serialize(Resoult);
			Assert.Equal(ExpedetText.ToLower(), resultText.ToLower());
		}
	  	public static IEnumerable<object[]> CreateAndGet_VehicleData()
        {
			DateTime dateTime = DateTime.Now;
			yield return new object[] {
			new Vehicle
			{
				Licenseplate = "CF24542",
				ModelType = "Diesel",
				RegisteringsDate = dateTime,
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
