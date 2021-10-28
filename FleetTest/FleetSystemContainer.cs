using System;
using System.Data;
using System.Data.SQLite;
using System.Net.Http;
using System.Threading.Tasks;
using Fleet;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace FleetTest
{
    public class FleetSystemContainer : IDisposable
    {
        protected readonly IServiceScope scope;
        protected readonly HttpClient TestClient;
        protected FleetSystemContainer()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(IDbConnection));
                        services.RemoveAll(typeof(IDbConnectionFactory));
                        services.AddSingleton<IDbConnection>(options => new SQLiteConnection(":memory:"));
                        services.AddSingleton<IDbConnectionFactory>(options => {
                            IDbConnection connection = options.GetService<IDbConnection>();
                            OrmLiteConnectionFactory connectionFactory = new(connection.ConnectionString, SqliteDialect.Provider);
                            return connectionFactory;
                        });
                    });
                });
            TestClient = appFactory.CreateClient();
            scope = appFactory.Services.CreateScope();
        }

        public void Dispose()
        {
            scope.Dispose();
        }
    }
}
