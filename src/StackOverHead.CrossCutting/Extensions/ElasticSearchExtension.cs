using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Nest;

namespace StackOverHead.CrossCutting.Extensions
{
    public static class ElasticSearchExtension
    {
        public static IServiceCollection AddElasticSearch(this IServiceCollection services, IConfiguration configuration)
        {
            var url = configuration["ElasticConfiguration:Uri"];
            var uri = new Uri(url);
            var defaultIndex = configuration["ElasticConfiguration:index"];

            var settings = new ConnectionSettings(uri)
                .DefaultIndex(defaultIndex);
            var client = new ElasticClient(settings);
            services.AddSingleton<IElasticClient>(client);
            return services;
        }
    }
}