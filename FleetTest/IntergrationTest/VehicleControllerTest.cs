using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Fleet.DataBaseLayre.Models;
using Xunit;
using System.Data;
using InterfacesLib;
using Fleet.DataBaseLayre;

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
		public async Task CreateAll_Vehicles(Vehicle vehicle)
		{
			//Arrange
			var services = scope.ServiceProvider;
			//Act
			var resoult = await TestClient.PostAsJsonAsync("api/Vehicle", vehicle);
			//Assert
			Assert.True(resoult.IsSuccessStatusCode, await resoult.Content.ReadAsStringAsync());
			string resultText = await resoult.Content.ReadAsStringAsync();
			Assert.NotEmpty(resultText);

			Vehicle resultObject = JsonConvert.DeserializeObject<Vehicle>(resultText);
			Assert.Equal(resultObject, vehicle);

			IRepository<Vehicle, int> db = services.GetService(typeof(IRepository<Vehicle, int>)) as IRepository<Vehicle, int>;

			Assert.Equal(await db.GetAsync(resultObject.Id), resultObject);
		}


		[Theory]
		[MemberData(nameof(CreateAndGet_VehicleData))]
		public async Task Get_Vehicle(Vehicle vehicle, Vehicle Resoult)
		{
			//Arrange
			var services = scope.ServiceProvider;
			FleetRepository db = services.GetService(typeof(FleetRepository)) as FleetRepository;
			var test = await db.UpsertAsync(vehicle);
			Resoult.Id = (int)test;
			//Act
			HttpResponseMessage response = await TestClient.GetAsync($"api/Vehicle/{test}");

			//Assert
			Assert.True(response.IsSuccessStatusCode, await response.Content.ReadAsStringAsync());

			string resultText = await response.Content.ReadAsStringAsync();
			Assert.NotEmpty(resultText);

			string ExpedetText = JsonConvert.SerializeObject(Resoult);
			Assert.Equal(resultText, ExpedetText);
		}
	  	public static IEnumerable<object[]> CreateAndGet_VehicleData()
        {
			yield return new object[] { 
			new Vehicle
			{
				Licenseplate = "CF24542",
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
			},new Vehicle
			{
				Licenseplate = "CF24542",
				Make = new Make
				{
					Id = 1,
					Name = "Chevrolet"
				},
				MakeId = 0,
				Model = new Model
				{
					Id = 1,
					Name = "Spark",
					Variant = "1.0"
				},
				ModelId = 0,
				VehicleType = new VehicleType
				{
					Id = 1,
					Name = "Personbil"
				},
				VehicleTypeId = 0,
				Vinnummer = "1HGBH41JXMN109186"
			}
			};
        }
	}
}
