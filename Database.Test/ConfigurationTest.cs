using Infrastructure.Test.Utils;
using FluentAssertions;
using System;
using Xunit;
using Xunit.Abstractions;

namespace Infrastructure.Test
{
    public class ConfigurationTests
    {
        /// <summary>
        /// Output
        /// </summary>
        private readonly ITestOutputHelper _output;

        /// <summary>
        /// C'tor
        /// </summary>
        public ConfigurationTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Can_Get_Configuration_Connection()
        {
            ConfigurationHelper helper = new ConfigurationHelper();
            var configuration = helper.GetConfiguration();

            configuration
                .Should().NotBeNull("Configuration object cannot be null");

            configuration
                .DapperTesting
                .ConnectionString
                .Should().NotBeNullOrEmpty("Connection string cannot be null or empty.");

            _output.WriteLine($"Connection string is: {configuration.DapperTesting.ConnectionString}");
        }

    }
}
