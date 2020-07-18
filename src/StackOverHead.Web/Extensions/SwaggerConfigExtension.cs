using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace StackOverHead.Web.Extensions
{
    public static class SwaggerConfigExtension
    {
        private const string SITE = "https://www.blogdoft.com.br";

        public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
        {
            var info = new OpenApiInfo
            {
                Title = "StackOverHead",
                Version = "v1",
                Description = "A Knowledge Management site",
                Contact = new OpenApiContact
                {
                    Name = "Francisco Thiago de Almeida",
                    Url = new Uri(SITE),
                },
            };
            services.AddSwaggerGen(config => config.SwaggerDoc(info.Version, info));
            return services;
        }
    }
}
