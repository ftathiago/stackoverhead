using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using StackOverHead.Question.App.Command;
using StackOverHead.Question.App.Models;
using StackOverHead.Question.Domain.Events;
using StackOverHead.Question.Elastic.Handlers;
using StackOverHead.Question.Elastic.Mapper;
using StackOverHead.Question.Elastic.Repositories;
using StackOverHead.Question.Elastic.Repositories.Impl;
using System;
using System.Collections.Generic;

namespace StackOverHead.Question.Elastic.Extensions
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddQuestionElastic(
            this IServiceCollection services,
            Type type,
            IConfiguration configuration)
        {
            return services
                .AddEventHandlers()
                .AddRepositories()
                .AddElasticSearch(configuration)
                .AddMapping(type);
        }

        private static IServiceCollection AddEventHandlers(this IServiceCollection services)
        {
            return services
                .AddScoped<INotificationHandler<RegisteredQuestion>, QuestionEventsHandler>()
                .AddScoped<INotificationHandler<RegisteredAnswer>, AnswerEventsHandler>()
                .AddScoped<INotificationHandler<RegisteredQuestionComment>, CommentEventsHandler>()
                .AddScoped<INotificationHandler<RegisteredAnswerComment>, CommentEventsHandler>()
                .AddScoped<IRequestHandler<QuestionCommand, IEnumerable<SearchQuestionResponse>>, SearchQuestionHandler>();
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services) =>
            services
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

        private static IServiceCollection AddMapping(this IServiceCollection services, Type type) =>
            services
                .AddAutoMapper(config => config.AddProfile<MappingProfile>(), type);
    }
}