using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Route.Test
{
    public class RouteSystemContainer
    {
        protected readonly WebApplicationFactory<Startup> app;
        protected readonly HttpClient TestClient;
        
        protected RouteSystemContainer()
        {
            app = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        //services.RemoveAll(typeof(IDbConnection));
                        //services.RemoveAll(typeof(IDbConnectionFactory));
                        //services.AddSingleton<IDbConnection>(options => new SQLiteConnection(":memory:"));
                        //services.AddSingleton<IDbConnectionFactory>(options =>
                        //{
                        //    IDbConnection connection = options.GetService<IDbConnection>();
                        //    OrmLiteConnectionFactory connectionFactory = new(connection.ConnectionString, SqliteDialect.Provider);
                        //    return connectionFactory;
                        //});
                    });
                });
            TestClient = app.CreateClient();
        }
    }
}
