using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using Fleet.DataBaseLayre;
using ServiceStack.OrmLite;
using System.ComponentModel;
using System.Data.SqlClient;
using ServiceStack.Data;
using ServiceStack.OrmLite.Sqlite;
using ServiceStack.OrmLite.SqlServer;

namespace Fleet
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			var sqLiteConnection = new SqlConnection("Data Source=den1.mssql7.gear.host;Integrated Security=false;User ID=aasemterprosjekt;Password=Jj2E8-?GYtyq;");

			services.AddSingleton<IDbConnection>(sqLiteConnection);
			services.AddSingleton<FleetRepository>();

			OrmLiteConfig.DialectProvider = new SqlServerOrmLiteDialectProvider();
			OrmLiteConfig.DialectProvider.NamingStrategy = new OrmLiteNamingStrategyBase();

			services.AddSingleton<IDbConnectionFactory>(c =>
			{
				var connectionFactory = new OrmLiteConnectionFactory(c.GetRequiredService<IDbConnection>().ConnectionString);

				return connectionFactory;
			});

			services.AddControllers();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Fleet", Version = "v1" });
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fleet v1"));
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
