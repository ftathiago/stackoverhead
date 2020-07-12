using System;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using StackOverHead.Auth.Domain.Repositories;
using StackOverHead.Auth.Infra.Factories;
using StackOverHead.Auth.Infra.Mapping.Profiles;
using StackOverHead.Auth.Infra.Repositories;

namespace StackOverHead.Auth.Infra.Extensions
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddAuthInfraDependencies(this IServiceCollection services, Type type) =>
            services
                .AddScoped<IUserRepository, UserRepository>()
                .AddTransient<IUserEntityModelFactory, UserEntityModelFactory>()
                .AddAutoMapper(config => config.AddProfile<UserProfile>(), type);
    }
}