using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Fleet.DataBaseLayre.Models;
using Xunit;
using System.Data;
using InterfacesLib;

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
			Assert.True(response.IsSuccessStatusCode);

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

			//Act
			var resoult = await TestClient.PostAsJsonAsync("api/Vehicle", vehicle);
			//Assert
			Assert.True(resoult.IsSuccessStatusCode);
			string resultText = await resoult.Content.ReadAsStringAsync();
			Assert.NotEmpty(resultText);

			Vehicle resultObject = JsonConvert.DeserializeObject<Vehicle>(resultText);
			Assert.Equal(resultObject, vehicle);

			IRepository<Vehicle, int> db = services.GetService(typeof(IRepository<Vehicle, int>)) as IRepository<Vehicle, int>;

			Assert.Equal(await db.GetAsync(resultObject.Id), resultObject);
		}


		[Theory]
		[MemberData(nameof(CreateAndGet_VehicleData))]
		public async Task CreateAndGet_Vehicle(Vehicle vehicle)
		{
			//Arrange
			IRepository<Vehicle, int> db = services.GetService(typeof(IRepository<Vehicle, int>)) as IRepository<Vehicle, int>;
			await db.UpsertAsync(vehicle);
			//Act
			HttpResponseMessage response = await TestClient.GetAsync($"api/Vehicle/{vehicle.Vinnummer}");

			//Assert
			Assert.True(response.IsSuccessStatusCode);

			string resultText = await response.Content.ReadAsStringAsync();
			Assert.NotEmpty(resultText);

			Vehicle resultObject = JsonConvert.DeserializeObject<Vehicle>(resultText);
			Assert.Equal(resultObject, vehicle);
		}
	  	public static IEnumerable<object[]> CreateAndGet_VehicleData()
        {
			yield return new object[] { new Vehicle
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
			}};
        }
	}
}
