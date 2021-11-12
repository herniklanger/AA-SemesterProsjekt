using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fleet.DataBaseLayre.Models.MessageBus;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Fleet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).ConfigureServices(services => 
            {
                services.AddMassTransit(x =>
                {
                    x.AddConsumer<MessagesConsumer>();
                    x.UsingRabbitMq((context, cfg) =>
                    {
                        cfg.ConfigureEndpoints(context);
                        cfg.Host("rabbitmq://localhost", h =>
                        {
                            // h.Username("aasemterprosjekt");
                            // h.Password("Jj2E8-?GYtyq");
                        });
                    });
                });
                services.AddMassTransitHostedService();
            }).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
