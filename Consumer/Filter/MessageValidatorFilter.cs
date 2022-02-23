using FluentValidation;
using GreenPipes;
using Logger;
using MassTransit;
using MassTransit.Definition;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer.Filter
{
    public class MessageValidatorFilter<T> : IFilter<ConsumeContext<T>>
    where T : class
    {
        private readonly ILoggerManager _logger;
        private readonly IValidator<T> _validator;

        public MessageValidatorFilter(ILoggerManager logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _validator = serviceProvider.GetService<IValidator<T>>() ?? throw new ArgumentNullException(nameof(_validator));
        }

        public async Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
        {
            var validationResult = await _validator.ValidateAsync(context.Message, context.CancellationToken) ?? new FluentValidation.Results.ValidationResult();

            if (!validationResult.IsValid)
            {
                string errorMessage = string.Join(string.Empty, validationResult.Errors.Select(x => x.ErrorMessage));
                _logger.LogError($"Message validation errors: {errorMessage}");

                //send message to custom queue or throw exception for publish message to error queue or both or only return
                await context.Send(
                    destinationAddress: new Uri($"queue:{new ExchangeNameFormatter().GenerateStandardClassName(typeof(T))}_validation_errors"),
                    message: new ValidationResultMessage<T>(context.Message, validationResult)
                );

                //throw custome exception

                return;
            }

            await next.Send(context);
        }

        public void Probe(ProbeContext context) { }
    }
}
