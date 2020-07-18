using StackOverHead.Question.App.Command;
using StackOverHead.Question.Domain.Enums;
using StackOverHead.Question.Elastic.Models;

namespace StackOverHead.Question.Elastic.Tests.Fixtures
{
    public class SearchQuestionFixture : BaseFixture
    {
        public QuestionCommand GetQuestionCommand() =>
            new QuestionCommand(
                    Faker().Lorem.Paragraphs(),
                    Faker().Lorem.Word(),
                    Faker().Random.Int(min: 0),
                    Faker().Random.Int(min: 10));

        public Answer GetAnswerFromElastic() =>
            new Answer
            {
                Id = Faker().Random.Guid(),
                AnswerKind = Faker().Random.Enum<AnswerKind>(),
                Content = Faker().Lorem.Paragraphs(),
                Tags = Faker().Random.Word(),
                QuestionId = Faker().Random.Guid(),
            };
    }
}