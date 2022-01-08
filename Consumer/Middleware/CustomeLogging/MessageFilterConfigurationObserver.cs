using Logger;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.PipeConfigurators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer.Middleware.CustomeLogging
{
    public class MessageFilterConfigurationObserver :ConfigurationObserver, IMessageConfigurationObserver
    {
        protected ILoggerManager _logger { get; }
        public MessageFilterConfigurationObserver(IConsumePipeConfigurator receiveEndpointConfigurator, ILoggerManager logger)
            : base(receiveEndpointConfigurator)
        {
            _logger = logger;
            Connect(this);
        }

        public void MessageConfigured<TMessage>(IConsumePipeConfigurator configurator)
            where TMessage : class
        {
            var specification = new MessageFilterPipeSpecification<TMessage>(_logger);

            configurator.AddPipeSpecification(specification);
        }
    }

}
