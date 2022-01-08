using Serilog;
using System;

namespace Logger
{
    public class LoggerManager : ILoggerManager
    {
        public void LogDebug(string message)
        {
            try
            {
                Log.Logger.Debug(message);
            }
            catch (Exception ex)
            {
                //
            }

        }

        public void LogError(string message)
        {

            try
            {
                Log.Logger.Error(message);
            }
            catch (Exception ex)
            {
                //
            }

        }

        public void LogInfo(string message)
        {

            try
            {
                Log.Logger.Information(message);
            }
            catch (Exception ex)
            {
                //
            }

        }

        public void LogInfo(UserRequestProperties request)
        {
            try
            {
                Log.Logger
                   .ForContext("MessageBody", request.MessageBody)
                   .ForContext("Code", request.Code)
                   .ForContext("Duration", request.Duration)
                   .ForContext("LogDate", DateTime.Now)
                   .ForContext("Response", request.Response)
                   .ForContext("Status", request.Status)
                   .ForContext("Url", request.Url)
                   .ForContext("ConsumerType", request.ConsumerType)
                   .Information(request.LogMessage);
            }
            catch (Exception ex)
            {
                //
            }
        }

        public void LogError(UserRequestProperties request)
        {
            try
            {
                string exception = Newtonsoft.Json.JsonConvert.SerializeObject(request.Exception, new Newtonsoft.Json.JsonSerializerSettings() { MaxDepth = 10 });

                Log.Logger
                    .ForContext("MessageBody", request.MessageBody)
                    .ForContext("Code", request.Code)
                    .ForContext("Duration", request.Duration)
                    .ForContext("LogDate", DateTime.Now)
                    .ForContext("Response", request.Response)
                    .ForContext("Status", request.Status)
                    .ForContext("Url", request.Url)
                    .ForContext("ConsumerType", request.ConsumerType)
                    .ForContext("Exception", exception)
                    .Error(request.Exception, request.LogMessage);
            }
            catch (Exception ex)
            {
                //
            }

        }

        public void LogWarn(string message)
        {

            try
            {
                Log.Logger.Warning(message);
            }
            catch (Exception ex)
            {
                //
            }

        }

        public void LogWarn(UserRequestProperties request)
        {

            try
            {
                string exception = Newtonsoft.Json.JsonConvert.SerializeObject(request.Exception, new Newtonsoft.Json.JsonSerializerSettings() { MaxDepth = 10 });

                Log.Logger
                   .ForContext("MessageBody", request.MessageBody)
                   .ForContext("Code", request.Code)
                   .ForContext("Duration", request.Duration)
                   .ForContext("LogDate", DateTime.Now)
                   .ForContext("Response", request.Response)
                   .ForContext("Status", request.Status)
                   .ForContext("Url", request.Url)
                   .ForContext("ConsumerType", request.ConsumerType)
                   .ForContext("Exception", exception)
                   .Warning(request.Exception, request.LogMessage);
            }
            catch (Exception ex)
            {
                //
            }

        }

    }


}
