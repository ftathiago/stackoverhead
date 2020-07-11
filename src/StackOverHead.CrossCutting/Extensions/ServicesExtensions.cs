using System;

using Microsoft.Extensions.DependencyInjection;

using AutoMapper;

using MediatR;

using StackOverHead.LibCommon.Events;
using StackOverHead.Question.Domain.Command;
using StackOverHead.Question.Domain.CommandHandlers;
using StackOverHead.Question.Domain.Repositories;
using StackOverHead.Question.Infra.Mapping.Profiles;
using StackOverHead.Question.Infra.Repositories;
using StackOverHead.Question.App.Services;
using StackOverHead.Question.App.Services.Impl;
using StackOverHead.Question.Infra.Factories.Impl;
using StackOverHead.Question.Infra.Factories;
using StackOverHead.Auth.Infra.Factories;
using StackOverHead.Auth.App.Services;
using StackOverHead.Auth.App.Services.Impl;
using StackOverHead.Auth.Domain.Repositories;
using StackOverHead.Auth.Infra.Repositories;
using StackOverHead.Auth.Infra.Mapping.Profiles;
using StackOverHead.Question.App.Factories;
using StackOverHead.Question.App.Factories.Impl;
using StackOverHead.Question.Domain.Events;
using StackOverHead.Question.Elastic.EventSourcings;
using StackOverHead.Question.Elastic.Repositories;
using StackOverHead.Question.Elastic.Repositories.Impl;
using StackOverHead.Question.Domain.Lib.Impl;
using StackOverHead.Question.Domain.Lib;

namespace StackOverHead.CrossCutting.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IAnswerRepository, AnswerRepository>();
            services.AddScoped<INotificationHandler<RegisteredQuestion>, ElasticEvents>();
            services.AddScoped<INotificationHandler<RegisteredAnswer>, ElasticEvents>();
            services.AddScoped<INotificationHandler<RegisteredAnswerComment>, ElasticEvents>();
            services.AddScoped<INotificationHandler<RegisteredQuestionComment>, ElasticEvents>();
            services.AddScoped<IElasticRepository, ElasticRepository>();
            return services;
        }

        public static IServiceCollection AddSystemDependencies(this IServiceCollection services,
            Type type)
        {
            services.AddMediatR(type);
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            return services;
        }

        public static IServiceCollection AddQuestionDependencies(this IServiceCollection services,
            Type type)
        {
            services.AddMediatR(type);
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            //.App
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddTransient<IQuestionResponseFactory, QuestionResponseFactory>();
            //.Domain
            services.AddScoped<IRequestHandler<RegisterQuestionCommand, bool>, QuestionCommandHandler>();
            services.AddScoped<IRequestHandler<RegisterAnswerCommand, bool>, AnswerCommandHandler>();
            services.AddScoped<IRequestHandler<RegisterAnswerCommentCommand, bool>, CommentCommandHandler>();
            services.AddScoped<IRequestHandler<RegisterQuestionCommentCommand, bool>, CommentCommandHandler>();
            services.AddTransient<IQuestionEventLauncher, QuestionEventLauncher>();
            //.Infra
            services.AddTransient<IQuestionEntityModelFactory, QuestionEntityModelFactory>();
            services.AddTransient<IAnswerEntityModelFactory, AnswerEntityModelFactory>();
            services.AddTransient<ICommentEntityToModelFactory, CommentEntityToModelFactory>();

            return services;
        }

        public static IServiceCollection AddAuthDependencies(this IServiceCollection services)
        {
            //.App
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            //.Domain
            services.AddScoped<IUserRepository, UserRepository>();
            //.Infra
            services.AddTransient<IUserEntityModelFactory, UserEntityModelFactory>();

            return services;
        }

        public static IServiceCollection AddMapper(this IServiceCollection services,
            Type type)
        {
            services.AddAutoMapper(config =>
            {
                config.AddProfile<UserProfile>();
                config.AddProfile<QuestionProfile>();
            }, type);
            return services;
        }
    }
}
