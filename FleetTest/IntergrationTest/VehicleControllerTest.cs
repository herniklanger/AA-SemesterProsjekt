using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Fleet.DataBaseLayre.Models;
using Xunit;

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


		[Fact]
		public async Task CreateAndGet_Vehicle()
		{
			//Arrange
			var vehicle = new Vehicle
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
			};

			//Act
			var _ = await TestClient.PostAsJsonAsync("api/Vehicle", vehicle);
			HttpResponseMessage response = await TestClient.GetAsync($"api/Vehicle/{vehicle.Vinnummer}");

			//Assert
			Assert.True(response.IsSuccessStatusCode);

			string resultText = await response.Content.ReadAsStringAsync();
			Assert.NotEmpty(resultText);

			Vehicle resultObject = JsonConvert.DeserializeObject<Vehicle>(resultText);
			Assert.Equal(resultObject, vehicle);
		}
	}
}
