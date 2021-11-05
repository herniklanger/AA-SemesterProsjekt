using Fleet.DataBaseLayre;
using Fleet.DataBaseLayre.Models;
using InterfacesLib;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using Fleet.DataBaseLayre.Interfaces;

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
            services.AddScoped<FleetRepository>();
            services.AddScoped<IRepository<Vehicle, int>>(x => x.GetService<FleetRepository>());
            services.AddScoped<IFleetRepository>(x => x.GetService<FleetRepository>());

            OrmLiteConfig.DialectProvider = new SqlServerOrmLiteDialectProvider();
            OrmLiteConfig.DialectProvider.NamingStrategy = new OrmLiteNamingStrategyBase();

            services.AddSingleton<IDbConnectionFactory>(c =>
            {
                var connection = c.GetRequiredService<IDbConnection>();

                IOrmLiteDialectProvider provider = new SqlServerOrmLiteDialectProvider();

                var connectionFactory = new OrmLiteConnectionFactory(connection.ConnectionString, provider);

                return connectionFactory;
            });

            services.AddControllers().AddNewtonsoftJson();
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
