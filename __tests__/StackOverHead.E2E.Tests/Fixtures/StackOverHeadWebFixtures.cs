using System;
using System.Reflection;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

using Serilog;
using Serilog.Events;

using StackOverHead.Web;
using StackOverHead.Web.Lib;

namespace StackOverHead.E2E.Tests.Fixtures
{
    public class StackOverHeadWebFixtures
    {
        public TestServer GetWebServer()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Integration");
            LogConfig();
            try
            {
                Log.Write(LogEventLevel.Information, "Initializing TestServer");
                var webHostBuilder = WebHost.CreateDefaultBuilder()
                        .UseEnvironment("Integration")
                        .ConfigureAppConfiguration(configuration =>
                            {
                                AddConfigurationSettings(configuration);
                            })
                        .UseSerilog()
                        .UseStartup<Startup>();
                var testServer = new TestServer(webHostBuilder);
                return testServer;
            }
            catch (Exception ex)
            {
                Log.Fatal($"Failed to start the {Assembly.GetExecutingAssembly().GetName().Name}", ex);
                throw;
            }
        }

        private void LogConfig()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var configuration = AddConfigurationSettings(new ConfigurationBuilder())
                .Build();
            var log = new LogConfigBuilder(configuration, environment);
            log.Build();
        }

        private IConfigurationBuilder AddConfigurationSettings(IConfigurationBuilder configuration)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            configuration
                .AddEnvironmentVariables(prefix: "TBS_")
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile(
                    $"appsettings.{environment}.json",
                    optional: true);
            return configuration;
        }
    }
}