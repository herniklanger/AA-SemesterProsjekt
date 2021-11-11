using System;
using System.Data;
using System.Data.SQLite;
using System.Net.Http;
using Driver;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace DriverTest
{
    internal class DriverSystemContainer
    {
        protected readonly WebApplicationFactory<Startup> app;
        protected readonly HttpClient TestClient;

        protected DriverSystemContainer()
        {
            app = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(IDbConnection));
                        services.RemoveAll(typeof(IDbConnectionFactory));
                        services.AddSingleton<IDbConnection>(options => new SQLiteConnection(":memory:"));
                        services.AddSingleton<IDbConnectionFactory>(options =>
                        {
                            IDbConnection connection = options.GetService<IDbConnection>();
                            OrmLiteConnectionFactory connectionFactory = new(connection.ConnectionString, SqliteDialect.Provider);
                            return connectionFactory;
                        });
                    });
                });
            TestClient = app.CreateClient();
        }
    }
}
