using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Newtonsoft.Json;
using System.Collections.Generic;
using Businesslogic;

namespace FleetTest
{
    public class VegicleControllerTest : FleetSystemContainer
    {
        [Fact]
        public async Task GetAll_Vegicles()
        {
            //Arrange

            //Act
            HttpResponseMessage response = await TestClient.GetAsync("api/Vehicle");
            //Assert
            Assert.True(response.IsSuccessStatusCode);
            string rsultText = await response.Content.ReadAsStringAsync();
            Assert.NotEmpty(rsultText);
            List<Vehicle> rsultObject = JsonConvert.DeserializeObject<List<Vehicle>>(rsultText);
            Assert.Empty(rsultObject);
        }
    }
}
