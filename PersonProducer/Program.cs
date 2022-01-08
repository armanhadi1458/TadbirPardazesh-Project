using Core.Dto;
using Logger;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonProducer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    IConfiguration configuration = hostContext.Configuration;

                    Log.Logger = new LoggerConfiguration()
                        .ReadFrom.Configuration(configuration)
                        .CreateLogger();

                    services.AddMassTransit(x =>
                    {
                        var rabbitConfig = configuration.GetSection(RabbitConfiguration.Name).Get<RabbitConfiguration>();

                        x.UsingRabbitMq((context, cfg) =>
                        {
                            cfg.Host(rabbitConfig.Host, "/",
                            h =>
                            {
                                h.Username(rabbitConfig.Username);
                                h.Password(rabbitConfig.Password);
                            });

                        });
                    });

                    services.AddHostedService<Worker>();
                });
    }
}
