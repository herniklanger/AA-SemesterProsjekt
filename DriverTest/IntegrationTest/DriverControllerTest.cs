﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Driver.DatabaseLayer;
using Driver.DatabaseLayer.Models;
using Driver.DatabaseLayer.Interfaces;
using Xunit;
using System;
using System.Data;
using System.Linq;
using InterfacesLib;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Dapper;

namespace DriverTest.IntegrationTest
{
    public class DriverControllerTest : DriverSystemContainer
    {
		[Fact]
		public async Task GetAll_Drivers()
		{
			//Arrange

			//Act
			HttpResponseMessage response = await TestClient.GetAsync("api/Driver");

			//Assert
			Assert.True(response.IsSuccessStatusCode, await response.Content.ReadAsStringAsync());
			string resultText = await response.Content.ReadAsStringAsync();
			Assert.NotEmpty(resultText);
			List<DriverModel> resultObject = JsonConvert.DeserializeObject<List<DriverModel>>(resultText);
			Assert.Empty(resultObject);
		}


		[Theory]
		[MemberData(nameof(CreateAndGet_DriverData))]
		public async Task Create_Vehicles(DriverModel driver)
		{
			var test = JsonConvert.SerializeObject(driver);
			//Arrange
			using (var scope = app.Server.Services.CreateScope())
			{
				//Act
				var resoult = await TestClient.PostAsJsonAsync("api/Driver", driver);
				//Assert
				Assert.True(resoult.IsSuccessStatusCode, await resoult.Content.ReadAsStringAsync());
				string resultText = await resoult.Content.ReadAsStringAsync();
				Assert.NotEmpty(resultText);

				DriverModel resultObject = JsonConvert.DeserializeObject<DriverModel>(resultText);

				IRepository<DriverModel, int> db = scope.ServiceProvider.GetService(typeof(DriverRepository)) as DriverRepository;
				DriverModel expedetResoult = await db.GetAsync(resultObject.Id);
				string expedetText = JsonConvert.SerializeObject(expedetResoult);
				Assert.Equal(expedetText.ToLower(), resultText.ToLower());
			}
		}


		[Theory]
		[MemberData(nameof(CreateAndGet_DriverData))]
		public async Task Get_Vehicle_By_Id(DriverModel driver)
		{
			//Arrange
			int Id;
			using (var scope = app.Services.CreateScope())
			{
				var services = scope.ServiceProvider;
				DriverRepository db = services.GetService(typeof(DriverRepository)) as DriverRepository;
				Id = await db.UpsertAsync(driver);
			}

			//Act
			HttpResponseMessage response = await TestClient.GetAsync($"api/Vehicle/{Id}");

			//Assert
			Assert.True(response.IsSuccessStatusCode, await response.Content.ReadAsStringAsync());

			string resultText = await response.Content.ReadAsStringAsync();
			Assert.NotEmpty(resultText);

			string ExpedetText = JsonConvert.SerializeObject(driver);
			Assert.Equal(ExpedetText.ToLower(), resultText.ToLower());
		}
		[Theory]
		[MemberData(nameof(CreateAndGet_DriverData))]
		public async Task Get_Driver_By_Driver_Name(DriverModel driver)
		{
			//Arrange
			var name = driver.Name;
			DriverModel[] Result = new[] { driver };
			using (var scope = app.Services.CreateScope())
			{
				var services = scope.ServiceProvider;
				DriverRepository db = services.GetService(typeof(DriverRepository)) as DriverRepository;

				int ObjectId = await db.UpsertAsync(driver);
				Result[0].Id = ObjectId/*{ await db.GetAsync(ObjectId) }*/;
			}
			driver.Name = null;
			driver.Role = null;
			driver.Email = null;
			driver.Surname = null;
			//Act
			HttpResponseMessage response = await TestClient.GetAsync($"api/driver/GetByName?name={name}");

			//Assert
			Assert.True(response.IsSuccessStatusCode, await response.Content.ReadAsStringAsync());

			string resultText = await response.Content.ReadAsStringAsync();
			Assert.NotEmpty(resultText);

			string ExpedetText = JsonConvert.SerializeObject(Result);
			Assert.Equal(ExpedetText.ToLower(), resultText.ToLower());
		}
		[Theory]
		[MemberData(nameof(CreateAndGet_DriverData))]
		public async Task Delete_Vehicle_By_Id(DriverModel driver)
		{
			//Arrange
			int Id;
			using (var scope = app.Services.CreateScope())
			{
				var services = scope.ServiceProvider;
				services.GetService(typeof(IDriverRepository));

				IDbConnection db = (services.GetService(typeof(IDbConnectionFactory)) as IDbConnectionFactory).OpenDbConnection();
				await db.SaveAsync(driver, true);//Add 1 driver
				int deleateId = driver.Id;
				driver.Id = 0;
				await db.SaveAsync(driver, true);//Add 2 drivers
				int Before = db.Query<int>("SELECT COUNT(*) FROM Driver").FirstOrDefault();

				//Act
				HttpResponseMessage response = await TestClient.DeleteAsync($"api/Driver/{deleateId}");

				//Assert
				Assert.True(response.IsSuccessStatusCode, await response.Content.ReadAsStringAsync());

				int Afer = db.Query<int>("SELECT COUNT(*) FROM Driver").FirstOrDefault();
				Assert.Equal(Before - 1, Afer);
			}
		}
		public static IEnumerable<object[]> CreateAndGet_DriverData()
		{
			yield return new object[] {
			new DriverModel
			{
				Email = "Jonny@newmail.mail",
				Name = "Jonny",
				Surname = "Bravo",
				Role = "driver",
				Id = 1
			}
			};
		}
	}
}
