using Core.Dto;
using Core.Utils;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
                                //add cluster for rabbit
                                //h.UseCluster(c =>
                                //{
                                //    c.Node("192.168.0.12");
                                //    c.Node("192.168.0.14");
                                //});
                            });

                            //custome serilizer
                            //cfg.SetMessageSerializer(new CustomeSerilizer());

                            cfg.ConfigureEndpoints(context);
                            cfg.MessageTopology.SetEntityNameFormatter(new ExchangeNameFormatter());
                        });
                    });

                    services.AddHostedService<Worker>();
                });
    }
}
