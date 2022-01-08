using GreenPipes;
using Logger;
using MassTransit;
using System.Collections.Generic;

namespace Consumer.Middleware.CustomeLogging
{
    public class MessageFilterPipeSpecification<T> : IPipeSpecification<ConsumeContext<T>>
    where T : class
    {
        protected ILoggerManager _logger { get; }
        public MessageFilterPipeSpecification(ILoggerManager logger)
        {
            _logger = logger;
        }
        public void Apply(IPipeBuilder<ConsumeContext<T>> builder)
        {
            var filter = new MessageFilter<T>(_logger);

            builder.AddFilter(filter);
        }

        public IEnumerable<ValidationResult> Validate()
        {
            yield break;
        }
    }

}
