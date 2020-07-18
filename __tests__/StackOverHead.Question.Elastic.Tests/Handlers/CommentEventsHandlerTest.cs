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
    public class CommentEventsHandlerTest : IClassFixture<BaseFixture>
    {
        private readonly Faker _faker;
        private readonly Mock<IAnswerRepository> _answerRepository;

        public CommentEventsHandlerTest(BaseFixture baseFixture)
        {
            _faker = baseFixture.Faker();
            _answerRepository = new Mock<IAnswerRepository>(MockBehavior.Strict);
        }

        [Fact]
        public async Task ShouldGenerateAnswerComment()
        {
            var expectedNotification = default(Answer);
            var notification = new RegisteredAnswerComment
            {
                Id = _faker.Random.Guid(),
                QuestionId = _faker.Random.Guid(),
                Content = _faker.Lorem.Paragraphs(),
            };
            _answerRepository
                .Setup(ar => ar.AddAsync(It.IsAny<Answer>()))
                .Callback<Answer>(answer => expectedNotification = answer)
                .Returns(Task.CompletedTask);
            var answerHandler = new CommentEventsHandler(_answerRepository.Object);

            await answerHandler
                .Handle(notification, new CancellationToken())
                .ConfigureAwait(false);

            expectedNotification.Should().BeEquivalentTo(notification, config =>
                config.Excluding(x => x.AnswerId));
            expectedNotification.AnswerKind.Should().Be(AnswerKind.Comment);
        }

        [Fact]
        public async Task ShouldGenerateQuestionComment()
        {
            var expectedAnswer = default(Answer);
            var notification = new RegisteredQuestionComment
            {
                Id = _faker.Random.Guid(),
                QuestionId = _faker.Random.Guid(),
                Content = _faker.Lorem.Paragraphs(),
            };
            _answerRepository
                .Setup(ar => ar.AddAsync(It.IsAny<Answer>()))
                .Callback<Answer>(answer => expectedAnswer = answer)
                .Returns(Task.CompletedTask);
            var answerHandler = new CommentEventsHandler(_answerRepository.Object);

            await answerHandler
                .Handle(notification, new CancellationToken())
                .ConfigureAwait(false);

            expectedAnswer.Should().BeEquivalentTo(notification);
            expectedAnswer.AnswerKind.Should().Be(AnswerKind.Comment);
        }
    }
}