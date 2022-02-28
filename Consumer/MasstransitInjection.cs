using Application.Person.NotificationHandlers;
using Consumer.Filter;
using Consumer.Middleware.CustomeLogging;
using Core.Dto;
using GreenPipes;
using Logger;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Consumer
{
    public static class MasstransitInjection
    {
        public static IServiceCollection AddMasstransit(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                var rabbitConfig = configuration.GetSection(RabbitConfiguration.Name).Get<RabbitConfiguration>();
               
                x.AddConsumer<CreatePersonSqlNotificationHandler>();
                x.AddConsumer<CreatePersonRedisNotificationHandler>();

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

                    cfg.ConfigureEndpoints(context);

                    cfg.MessageTopology.SetEntityNameFormatter(new ExchangeNameFormatter());

                    //add validation filter
                    cfg.UseConsumeFilter(typeof(MessageValidatorFilter<>), context);

                    //custome serilizer
                    //cfg.SetMessageSerializer(new CustomeSerilizer());

                    //uncomment for use retry exception
                    //cfg.UseMessageRetry(x =>
                    //{
                    //    x.Interval(3, TimeSpan.FromMilliseconds(1000));
                    //});

                    //using my custome middleware for loging and exception handling
                    //better approch because request first recieve in middleware
                    var logger = services.BuildServiceProvider().GetRequiredService<ILoggerManager>();
                    cfg.UseMessageFilter(logger);

                    //uncomment for using observer for request log for each step(not very interesting)
                    //cfg.ConnectReceiveObserver(new ReceiveObserver(logger));

                });
            });

            services.AddMassTransitHostedService();
            return services;
        }
    }
}
