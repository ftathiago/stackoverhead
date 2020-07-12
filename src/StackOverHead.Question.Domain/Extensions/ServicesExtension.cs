using MediatR;
using Microsoft.Extensions.DependencyInjection;
using StackOverHead.Question.Domain.Command;
using StackOverHead.Question.Domain.CommandHandlers;
using StackOverHead.Question.Domain.Lib;
using StackOverHead.Question.Domain.Lib.Impl;

namespace StackOverHead.Question.Domain.Extensions
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddQuestionDomain(this IServiceCollection services) =>
            services
                .AddScoped<IQuestionEventLauncher, QuestionEventLauncher>()
                .AddCommandHandlers();

        private static IServiceCollection AddCommandHandlers(this IServiceCollection services) =>
            services
                .AddScoped<IRequestHandler<RegisterQuestionCommand, bool>, QuestionCommandHandler>()
                .AddScoped<IRequestHandler<RegisterAnswerCommand, bool>, AnswerCommandHandler>()
                .AddScoped<IRequestHandler<RegisterAnswerCommentCommand, bool>, CommentCommandHandler>()
                .AddScoped<IRequestHandler<RegisterQuestionCommentCommand, bool>, CommentCommandHandler>();
    }
}