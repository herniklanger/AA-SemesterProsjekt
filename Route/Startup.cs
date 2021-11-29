using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Data;
using System.Data.SqlClient;
using InterfacesLib;
using Route.BusinesseLayre;
using Route.BusinesseLayre.Interfaces;
using Route.DataBaseLayre;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;

namespace Route
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var sqLiteConnection = new SqlConnection("Server=den1.mssql7.gear.host;Database=aaroutes;User Id=aaroutes;Password=Ki7u5!vgr3~a;");
            
            services.AddSingleton<IDbConnection>(sqLiteConnection);
            services.AddScoped<RouteRepository>();
            services.AddScoped<IRepository<DataBaseLayre.Models.Route, int>>(x => x.GetService<RouteRepository>());

            OrmLiteConfig.DialectProvider = new SqlServerOrmLiteDialectProvider();
            OrmLiteConfig.DialectProvider.NamingStrategy = new OrmLiteNamingStrategyBase();

            services.AddSingleton<IDbConnectionFactory>(c =>
            {
                var connection = c.GetRequiredService<IDbConnection>();

                IOrmLiteDialectProvider provider = new SqlServerOrmLiteDialectProvider();

                var connectionFactory = new OrmLiteConnectionFactory(connection.ConnectionString, provider);

                return connectionFactory;
            });
            services.AddScoped<IRouteCalculatore, RouteCalculatore>();
            services.AddControllers().AddNewtonsoftJson();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Route", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using(var scope = app.ApplicationServices.CreateScope())
                scope.ServiceProvider.GetService<RouteRepository>();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Route v1"));
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
