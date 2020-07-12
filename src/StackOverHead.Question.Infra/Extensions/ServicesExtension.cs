using System;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using StackOverHead.Question.Domain.Repositories;
using StackOverHead.Question.Infra.Factories;
using StackOverHead.Question.Infra.Factories.Impl;
using StackOverHead.Question.Infra.Mapping.Profiles;
using StackOverHead.Question.Infra.Repositories;

namespace StackOverHead.Question.Infra.Extensions
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddQuestionInfra(this IServiceCollection services, Type type) =>
            services
                .AddRepositories()
                .AddFactories()
                .AddMapping(type);

        private static IServiceCollection AddRepositories(this IServiceCollection services) =>
            services
                .AddScoped<IQuestionRepository, QuestionRepository>()
                .AddScoped<IAnswerRepository, AnswerRepository>();

        private static IServiceCollection AddFactories(this IServiceCollection services) =>
            services
                .AddScoped<IAnswerEntityModelFactory, AnswerEntityModelFactory>()
                .AddScoped<ICommentEntityToModelFactory, CommentEntityToModelFactory>()
                .AddScoped<IQuestionEntityModelFactory, QuestionEntityModelFactory>();

        private static IServiceCollection AddMapping(this IServiceCollection services, Type type) =>
            services
                .AddAutoMapper(config => config.AddProfile<QuestionProfile>(), type);
    }
}