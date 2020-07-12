using Microsoft.Extensions.DependencyInjection;

namespace StackOverHead.Auth.Domain.Extensions
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddAuthDomainDependencies(this IServiceCollection services) =>
            services;
    }
}