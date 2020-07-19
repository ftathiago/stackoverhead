// <copyright file="Program.cs" company="BlogDoFT">
// Copyright (c) BlogDoFT. All rights reserved.
// </copyright>

using System;
using System.Reflection;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

using Serilog;

using StackOverHead.CrossCutting.Extensions;
using StackOverHead.Web.Lib;

namespace StackOverHead.Web
{
    public static class Program
    {
        public static void Main()
        {
            LogConfig();
            try
            {
                Log.Debug("Starting application");
                CreateHostBuilder()
                    .Build()
                    .InitializeDataBase()
                    .Run();
            }
            catch (Exception ex)
            {
                Log.Fatal($"Failed to start the {Assembly.GetExecutingAssembly().GetName().Name}", ex);
                throw;
            }
        }

        public static IHostBuilder CreateHostBuilder() =>
            Host
                .CreateDefaultBuilder()
                .ConfigureAppConfiguration(configuration => AddConfigurationSettings(configuration))
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
                .UseSerilog();

        private static void LogConfig()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var configuration = AddConfigurationSettings(new ConfigurationBuilder())
                .Build();
            var log = new LogConfigBuilder(configuration, environment);
            log.Build();
        }

        private static IConfigurationBuilder AddConfigurationSettings(IConfigurationBuilder configuration)
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
