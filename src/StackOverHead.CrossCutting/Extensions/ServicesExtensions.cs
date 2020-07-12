using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using StackOverHead.Auth.App.Extensions;
using StackOverHead.Auth.Domain.Extensions;
using StackOverHead.Auth.Infra.Extensions;
using StackOverHead.LibCommon.Events;
using StackOverHead.Question.App.Extensions;
using StackOverHead.Question.Domain.Extensions;
using StackOverHead.Question.Elastic.Extensions;
using StackOverHead.Question.Infra.Extensions;

namespace StackOverHead.CrossCutting.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddDependencies(
            this IServiceCollection services,
            Type type,
            IConfiguration configuration) =>
            services
                .AddAuthDependencies(type)
                .AddSystemDependencies(type)
                .AddQuestionDependencies(type, configuration);

        private static IServiceCollection AddQuestionDependencies(
            this IServiceCollection services,
            Type type,
            IConfiguration configuration) =>
            services
                .AddQuestionInfra(type)
                .AddQuestionElastic(type, configuration)
                .AddQuestionDomain()
                .AddQuestionAppDependencies();

        private static IServiceCollection AddAuthDependencies(
            this IServiceCollection services,
            Type type) =>
            services
                .AddAuthAppDependencies()
                .AddAuthDomainDependencies()
                .AddAuthInfraDependencies(type);

        private static IServiceCollection AddSystemDependencies(this IServiceCollection services,
            Type type)
        {
            services.AddMediatR(type);
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            return services;
        }
    }
}
