using Microsoft.Extensions.DependencyInjection;
using StackOverHead.Auth.App.Services;
using StackOverHead.Auth.App.Services.Impl;

namespace StackOverHead.Auth.App.Extensions
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddAuthAppDependencies(this IServiceCollection services) =>
            services
                .AddScoped<IAuthenticationService, AuthenticationService>()
                .AddScoped<IUserService, UserService>();
    }
}