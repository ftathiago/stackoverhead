using Microsoft.Extensions.DependencyInjection;
using StackOverHead.Web.Services;
using StackOverHead.Web.Services.Impl;

namespace StackOverHead.Web.Extensions
{
    public static class DependenciesExtensions
    {
        public static IServiceCollection AddWebDependencies(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            return services;
        }
    }
}
