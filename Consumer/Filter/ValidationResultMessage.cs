using FluentValidation.Results;

namespace Consumer.Filter
{
    public record ValidationResultMessage<TMessage>(TMessage Message, ValidationResult ValidationResult);
}