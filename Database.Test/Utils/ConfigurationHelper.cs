using Microsoft.Extensions.Configuration;
using System;

namespace Infrastructure.Test.Utils
{
    public class ConfigurationHelper
    {
        /// <summary>
        /// C'tor
        /// </summary>
        public ConfigurationHelper()
        { }

        /// <summary>
        /// Get configuration root
        /// </summary>
        /// <param name="outputPath"></param>
        /// <returns></returns>
        private IConfigurationRoot GetConfigurationRoot()
        {
            return new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("settings.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }

        /// <summary>
        /// Get TestConfiguration
        /// </summary>
        public TestConfiguration GetConfiguration()
        {
            var configuration = new TestConfiguration();
            var iConfigRoot = GetConfigurationRoot();

            iConfigRoot
                //.GetSection("DapperTesting")
                .Bind(configuration);

            return configuration;
        }
    }
}