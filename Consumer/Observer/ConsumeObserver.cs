using Logger;
using MassTransit;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Consumer.Observer
{
    public class ReceiveObserver : IReceiveObserver
    {
        protected ILoggerManager _logger { get; }
        public ReceiveObserver(ILoggerManager logger)
        {
            _logger = logger;
        }
        public async Task PreReceive(ReceiveContext context)
        {
            string body = GetMessageBody(context);

            _logger.LogInfo(new UserRequestProperties()
            {
                LogMessage = $"Message receive at {context.InputAddress.AbsoluteUri}",
                MessageBody = body,
                Status = ERunningStatus.Beginning,
                Url = context.InputAddress.AbsoluteUri,
                Code = context.GetMessageId().ToString(),
            });

            await context.ReceiveCompleted;
        }

        public async Task PostReceive(ReceiveContext context)
        {
            string body = GetMessageBody(context);

            _logger.LogInfo(new UserRequestProperties()
            {
                LogMessage = $"Message proccess done in {context.InputAddress.AbsoluteUri}",
                MessageBody = body,
                Status = ERunningStatus.Done,
                Url = context.InputAddress.AbsoluteUri,
                Code = context.GetMessageId().ToString(),
            });

            await context.ReceiveCompleted;
        }

        public async Task PostConsume<T>(ConsumeContext<T> context, TimeSpan duration, string consumerType)
            where T : class
        {
            string body = GetMessageBody(context.ReceiveContext);

            _logger.LogInfo(new UserRequestProperties()
            {
                LogMessage = $"Message consume start in {context.ReceiveContext.InputAddress.AbsoluteUri}",
                Duration = duration.ToString(),
                MessageBody = body,
                Status = ERunningStatus.Running,
                Url = context.ReceiveContext.InputAddress.AbsoluteUri,
                Code = context.ReceiveContext.GetMessageId().ToString()
            });

            await context.ConsumeCompleted;
        }

        public async Task ConsumeFault<T>(ConsumeContext<T> context, TimeSpan elapsed, string consumerType, Exception exception) where T : class
        {
            string body = GetMessageBody(context.ReceiveContext);

            _logger.LogError(new UserRequestProperties()
            {
                LogMessage = $"Message consume failed in {context.ReceiveContext.InputAddress.AbsoluteUri}",
                Duration = elapsed.ToString(),
                MessageBody = body,
                Status = ERunningStatus.Failed,
                Url = context.ReceiveContext.InputAddress.AbsoluteUri,
                Code = context.ReceiveContext.GetMessageId().ToString(),
                Exception = exception
            });

            await context.ConsumeCompleted;
        }

        public async Task ReceiveFault(ReceiveContext context, Exception exception)
        {
            string body = GetMessageBody(context);

            _logger.LogError(new UserRequestProperties()
            {
                LogMessage = $"Message receive failed in {context.InputAddress.AbsoluteUri}",
                MessageBody = body,
                Status = ERunningStatus.Failed,
                Url = context.InputAddress.AbsoluteUri,
                Code = context.GetMessageId().ToString(),
                Exception = exception
            });

            await context.ReceiveCompleted;
        }


        private string GetMessageBody(ReceiveContext context)
        {
            string body = string.Empty;
            using (var sr = new StreamReader(context.GetBodyStream()))
            {
                body = sr.ReadToEnd();
            }
            return body;
        }

    }
}
