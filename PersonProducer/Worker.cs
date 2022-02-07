using Application.Person.Notifications;
using Core.Utils;
using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace PersonProducer
{
    public class Worker : BackgroundService
    {
        protected ILogger<Worker> _logger { get; }
        protected IBusControl _busControl { get; }


        public Worker(ILogger<Worker> logger, IBusControl busControl)
        {
            _logger = logger;
            _busControl = busControl;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Producer Service Start At {DateTime.Now} ...");
            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Random random = new Random();
                int personPrefix = random.Next(100, 1000);

                var person = new CreatePersonNotification()
                {
                    Age = random.Next(16, 90),
                    FirstName = $"FirstName_{personPrefix}",
                    LastName = $"lastName_{personPrefix}"
                };

                //for debug validation uncomment this and comment above code
                //var person = new CreatePersonNotification()
                //{
                //    Age = 10,
                //    FirstName = "",
                //    LastName = ""
                //};

                await _busControl.Publish(person);

                Console.WriteLine($"Message Published \n Person : {JsonSerializer.Serialize(person, JsonSerializerSetting.JsonSerializerOptions)}");
                Console.WriteLine("----------------------------------------------------------------------------");

                await Task.Delay(5000, stoppingToken);
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Producer Service Stop At {DateTime.Now} ...");
            return base.StopAsync(cancellationToken);
        }

    }
}
