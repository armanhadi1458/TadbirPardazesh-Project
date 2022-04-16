using GreenPipes;
using Logger;
using MassTransit;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Consumer.Middleware.CustomeLogging
{
    public class MessageFilter<T> : IFilter<ConsumeContext<T>>
        where T : class
    {
        protected ILoggerManager _logger { get; }
        public MessageFilter(ILoggerManager logger)
        {
            _logger = logger;
        }
        public void Probe(ProbeContext context)
        {
            
        }

        public async Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
        {
            var logModel = CreateBegginigLogModel(context);
            _logger.LogInfo(logModel);

            Stopwatch sw = new Stopwatch();
            sw.Start();

            try
            {
                await next.Send(context);
                sw.Stop();

                logModel.LogMessage = $"Message proccess done in {context.ReceiveContext.InputAddress.AbsoluteUri}";
                logModel.Duration = sw.ElapsedMilliseconds.ToString();
                logModel.Status = ERunningStatus.Done;
                _logger.LogInfo(logModel);
            }
            catch (Exception ex)
            {
                sw.Stop();
                logModel.LogMessage = $"Message consume failed in {context.ReceiveContext.InputAddress.AbsoluteUri}";
                logModel.Duration = sw.ElapsedMilliseconds.ToString();
                logModel.Status = ERunningStatus.Failed;
                logModel.Exception = ex;
                _logger.LogError(logModel);
                //Your Custome Handle Here ... 
                throw;
            }
        }

        private UserRequestProperties CreateBegginigLogModel(ConsumeContext context)
        {
            string messageBody = GetMessageBody(context.ReceiveContext);

            var logModel = new UserRequestProperties()
            {
                LogMessage = $"Message receive in {context.ReceiveContext.InputAddress.AbsoluteUri}",
                MessageBody = messageBody,
                Status = ERunningStatus.Beginning,
                Url = context.ReceiveContext.InputAddress.AbsoluteUri,
                Code = context.ReceiveContext.GetMessageId().ToString(),
            };

            return logModel;
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
