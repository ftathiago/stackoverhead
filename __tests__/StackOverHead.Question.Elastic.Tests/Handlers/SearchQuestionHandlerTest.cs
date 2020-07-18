using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using Moq;
using StackOverHead.Question.App.Command;
using StackOverHead.Question.Domain.Enums;
using StackOverHead.Question.Elastic.Handlers;
using StackOverHead.Question.Elastic.Models;
using StackOverHead.Question.Elastic.Repositories;
using StackOverHead.Question.Elastic.Tests.Fixtures;
using Xunit;

namespace StackOverHead.Question.Elastic.Tests.Handlers
{
    public class SearchQuestionHandlerTest :
        IDisposable,
        IClassFixture<SearchQuestionFixture>
    {
        private readonly Faker _faker;
        private readonly SearchQuestionFixture _fixture;
        private readonly Mock<IAnswerRepository> _answers;

        public SearchQuestionHandlerTest(SearchQuestionFixture fixture)
        {
            _faker = fixture.Faker();
            _fixture = fixture;
            _answers = new Mock<IAnswerRepository>(MockBehavior.Strict);
        }

        void IDisposable.Dispose()
        {
            _answers.Verify();
        }

        [Fact]
        public async Task ShouldReturnOneQuestionResponse()
        {
            var answersFound = new List<Answer>
            {
                _fixture.GetAnswerFromElastic()
            };
            var questionCommand = _fixture.GetQuestionCommand();
            _answers
                .Setup(a => a.SearchAsync(
                    questionCommand.Content,
                    questionCommand.Tags,
                    questionCommand.Page,
                    questionCommand.PageSize))
                .ReturnsAsync(answersFound)
                .Verifiable();
            var questionHandler = new SearchQuestionHandler(_answers.Object);

            var response = await questionHandler.Handle(questionCommand, new CancellationToken())
                .ConfigureAwait(false);

            response.Should().HaveCount(1);
            response.Should().BeEquivalentTo(answersFound);
        }

        [Fact]
        public void ShouldReturnMoreThanOneQuestionReponse()
        {

        }

        [Fact]
        public void ShouldReturnEmptyEnumerationWhenNoneQuestionFound()
        {

        }
    }
}