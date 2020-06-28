// <copyright file="LogConfigBuilder.cs" company="BlogDoFT">
// Copyright (c) BlogDoFT. All rights reserved.
// </copyright>

using System;
using System.Reflection;

using Microsoft.Extensions.Configuration;

using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Formatting.Json;
using Serilog.Sinks.Elasticsearch;

namespace StackOverHead.Web.Lib
{
    /// <summary>
    /// Configure Serilog to project.
    /// </summary>
    public class LogConfigBuilder
    {
        private readonly IConfigurationRoot _configuration;
        private readonly string _environment;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogConfigBuilder"/> class.
        /// </summary>
        /// <param name="configuration">A reference to app configuration params.</param>
        /// <param name="environment">Environment running.</param>
        public LogConfigBuilder(IConfigurationRoot configuration, string environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        /// <summary>
        /// Builds Log configuration (with Serilog).
        /// </summary>
        public void Build()
        {
            Log.Logger = GetLoggerConfiguration();
        }

        private ILogger GetLoggerConfiguration()
        {
            var logConfigBuilder = new LoggerConfiguration()
                   .Enrich.FromLogContext()
                   .Enrich.WithMachineName()
                   .Enrich.WithExceptionDetails()
                   .Enrich.WithProperty("Environment", _environment)
                   .WriteTo.Debug()
                   .WriteTo.Console()
                   .WriteTo.Elasticsearch(ConfigureElasticSink())
                   .ReadFrom.Configuration(_configuration);

            if (_environment != "Integration")
            {
                logConfigBuilder.WriteTo.File(new JsonFormatter(), GetLogFileName(), LogEventLevel.Debug);
            }

            return logConfigBuilder.CreateLogger();
        }

        private string GetLogFileName() => $"{GetLogIndentifier()}.log";

        private string GetLogIndentifier()
        {
            var logBaseName = Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-");
            var logByDay = $"{DateTime.UtcNow:yyyy-MM-dd}";
            return $"{logBaseName}-{_environment}-{logByDay}";
        }

        private ElasticsearchSinkOptions ConfigureElasticSink()
        {
            var uri = new Uri(_configuration["ElasticConfiguration:Uri"]);
            return new ElasticsearchSinkOptions(uri)
            {
                AutoRegisterTemplate = true,
                AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
                IndexFormat = GetLogIndentifier(),
                EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog |
                                       EmitEventFailureHandling.WriteToFailureSink |
                                       EmitEventFailureHandling.RaiseCallback,
            };
        }
    }
}
