using System;
using System.Data;
using System.Data.SQLite;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Fleet;
using MassTransit;
using MassTransit.Testing;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Moq;
using RabbitMQ.Client;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace FleetTest
{
    public class FleetSystemContainer
    {
        protected readonly WebApplicationFactory<Startup> app;
        protected readonly HttpClient TestClient;
        protected Mock<IBus> IBusMock;

        protected FleetSystemContainer()
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

                        services.AddMassTransitInMemoryTestHarness();
                        
                    });
                });
            TestClient = app.CreateClient();
            app.Server.Services.GetService<InMemoryTestHarness>().Start().Wait();
        }
    }
}
