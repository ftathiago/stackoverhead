using Bogus;
using FluentAssertions;
using Moq;
using StackOverHead.Question.Domain.Enums;
using StackOverHead.Question.Domain.Events;
using StackOverHead.Question.Elastic.Handlers;
using StackOverHead.Question.Elastic.Models;
using StackOverHead.Question.Elastic.Repositories;
using StackOverHead.Question.Elastic.Tests.Fixtures;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace StackOverHead.Question.Elastic.Tests.Handlers
{
    public class AnswerEventsHandlerTest : IClassFixture<BaseFixture>
    {
        private readonly Faker _faker;
        private readonly Mock<IAnswerRepository> _answerRepository;

        public AnswerEventsHandlerTest(BaseFixture baseFixture)
        {
            _faker = baseFixture.Faker();
            _answerRepository = new Mock<IAnswerRepository>(MockBehavior.Strict);
        }

        [Fact]
        public async Task ShouldGenerateAAnswer()
        {
            var expectedNotification = default(Answer);
            var notification = new RegisteredAnswer
            {
                Id = _faker.Random.Guid(),
                QuestionId = _faker.Random.Guid(),
                Content = _faker.Lorem.Paragraphs(),
            };
            _answerRepository
                .Setup(ar => ar.AddAsync(It.IsAny<Answer>()))
                .Callback<Answer>(answer => expectedNotification = answer)
                .Returns(Task.CompletedTask);
            var answerHandler = new AnswerEventsHandler(_answerRepository.Object);

            await answerHandler
                .Handle(notification, new CancellationToken())
                .ConfigureAwait(false);

            expectedNotification.Should().BeEquivalentTo(notification);
            expectedNotification.AnswerKind.Should().Be(AnswerKind.Answer);
        }
    }
}