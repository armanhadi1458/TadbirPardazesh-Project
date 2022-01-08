using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Infrastructure;
using Application;
using Microsoft.Extensions.Configuration;
using Serilog;
using Logger;

namespace Consumer
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

                    services.AddTransient<ILoggerManager, LoggerManagerSerilog>();

                    services.AddInfrastructure(configuration);
                    services.AddApplication(configuration);

                    services.AddMasstransit(configuration);
                    services.AddHostedService<Worker>();

                }).UseSerilog();
    }

}