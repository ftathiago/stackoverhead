using System;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using StackOverHead.Question.Domain.Events;
using StackOverHead.Question.Elastic.EventSourcings;
using StackOverHead.Question.Elastic.Repositories;
using StackOverHead.Question.Elastic.Repositories.Impl;

namespace StackOverHead.Question.Elastic.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddQuestionElastic(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddEventHandlers()
                .AddRepositories()
                .AddElasticSearch(configuration);
        }

        private static IServiceCollection AddEventHandlers(this IServiceCollection services)
        {
            return services
                .AddScoped<INotificationHandler<RegisteredQuestion>, QuestionEventsHandler>()
                .AddScoped<INotificationHandler<RegisteredAnswer>, AnswerEventsHandler>()
                .AddScoped<INotificationHandler<RegisteredQuestionComment>, CommentEventsHandler>()
                .AddScoped<INotificationHandler<RegisteredAnswerComment>, CommentEventsHandler>();
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services) => services
            .AddScoped<IAnswerRepository, Answers>();

        private static IServiceCollection AddElasticSearch(this IServiceCollection services, IConfiguration configuration)
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