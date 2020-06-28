// <copyright file="DependenciesExtensions.cs" company="BlogDoFT">
// Copyright (c) BlogDoFT. All rights reserved.
// </copyright>

using Microsoft.Extensions.DependencyInjection;

using StackOverHead.Web.Services;
using StackOverHead.Web.Services.Impl;

namespace StackOverHead.Web.Extensions
{
    /// <summary>
    /// Extension methods to add Web dependencies injections.
    /// </summary>
    public static class DependenciesExtensions
    {
        /// <summary>
        /// Add dependencies to serviceCollection.
        /// </summary>
        /// <param name="services">App ServiceColletion.</param>
        /// <returns>Just for fluent interface pattern.</returns>
        public static IServiceCollection AddWebDependencies(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            return services;
        }
    }
}
