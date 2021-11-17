using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Data.SqlClient;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using Driver.DatabaseLayer;
using InterfacesLib;
using System.Data;
using Driver.DatabaseLayer.Interfaces;

namespace Driver
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
            var sqLiteConnection = new SqlConnection("Server=den1.mssql7.gear.host;Database=aadriver;User Id=aadriver;Password=Tu4obAp-_1c2;");
            services.AddSingleton<IDbConnection>(sqLiteConnection);
            services.AddScoped<DriverRepository>();
            services.AddScoped<IRepository<DatabaseLayer.Models.DriverModel, int>>(x => x.GetService<DriverRepository>());
            services.AddScoped<IDriverRepository>(x => x.GetService<DriverRepository>());

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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Driver", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var scope = app.ApplicationServices.CreateScope())
                scope.ServiceProvider.GetService<DriverRepository>();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Driver v1"));
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
