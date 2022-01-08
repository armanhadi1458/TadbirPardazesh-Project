using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Consumer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            string msg = $"Comsumers Service Start At {DateTime.Now}";

            Console.WriteLine(msg);
            Console.WriteLine("Application started. Press Ctrl+C to shut down.");

            _logger.LogInformation(msg);

            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Consumers Service Stop At {DateTime.Now} ...");
            return base.StopAsync(cancellationToken);
        }
    }
}
