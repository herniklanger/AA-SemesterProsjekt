using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Newtonsoft.Json;
using System.Collections.Generic;
using Fleet.Businesslogic;

namespace FleetTest
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
    }
}
