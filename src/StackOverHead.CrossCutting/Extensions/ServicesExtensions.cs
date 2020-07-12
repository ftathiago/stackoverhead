using System;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using MediatR;
using StackOverHead.LibCommon.Events;
using StackOverHead.Question.Infra.Extensions;
using StackOverHead.Question.Elastic.Extensions;
using StackOverHead.Question.Domain.Extensions;
using StackOverHead.Question.App.Extensions;
using StackOverHead.Auth.App.Extensions;
using StackOverHead.Auth.Domain.Extensions;
using StackOverHead.Auth.Infra.Extensions;
using Microsoft.Extensions.Configuration;

namespace StackOverHead.CrossCutting.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddDependencies(
            this IServiceCollection services,
            Type type,
            IConfiguration configuration) =>
            services
                .AddQuestionDependencies(type, configuration)
                .AddAuthDependencies(type)
                .AddSystemDependencies(type);

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
