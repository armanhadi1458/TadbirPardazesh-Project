using Logger;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer.Middleware.CustomeLogging
{
    public static class MessageFilterConfigurationExtensions
    {
        public static void UseMessageFilter(this IConsumePipeConfigurator configurator, ILoggerManager logger)
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));

            var observer = new MessageFilterConfigurationObserver(configurator, logger);
        }
    }

}
