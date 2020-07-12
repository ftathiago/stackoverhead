using Microsoft.Extensions.DependencyInjection;
using StackOverHead.Question.App.Factories;
using StackOverHead.Question.App.Factories.Impl;
using StackOverHead.Question.App.Services;
using StackOverHead.Question.App.Services.Impl;

namespace StackOverHead.Question.App.Extensions
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddQuestionAppDependencies(this IServiceCollection services) =>
            services
                .AddScoped<IQuestionService, QuestionService>()
                .AddTransient<IQuestionResponseFactory, QuestionResponseFactory>();
    }
}