using FluentAssertions;
using Moq;
using Nest;
using StackOverHead.Question.Elastic.Exceptions;
using StackOverHead.Question.Elastic.Models;
using StackOverHead.Question.Elastic.Repositories.Impl;
using System;
using System.Threading.Tasks;
using Xunit;

namespace StackOverHead.Question.Elastic.Tests.Repositories
{
    public class AnswersTest :
            IDisposable
    {
        private readonly Mock<IElasticClient> _client;

        public AnswersTest()
        {
            _client = new Mock<IElasticClient>(MockBehavior.Strict);
        }

        public void Dispose()
        {
            _client.Verify();
        }

        [Fact]
        public async Task ShouldThrowDocumentNotFoundWhenTryingUpdateANotFoundDocument()
        {
            var model = new Answer { Id = Guid.NewGuid() };
            var response = new GetResponse<Answer>();
            _client
                .Setup(c => c.GetAsync<Answer>(model.Id, null, default))
                .ReturnsAsync(response)
                .Verifiable();
            var answers = new Answers(_client.Object);

            Func<Task> act = async () => await answers.UpdateAsync(model)
                .ConfigureAwait(false);

            await act.Should().ThrowAsync<DocumentNotFoundElkException>()
                .ConfigureAwait(false);
        }

        [Fact]
        public async Task ShouldUpdateExistingAnswer()
        {/** It's not possible mock response */}

        [Fact]
        public void ShouldReturnEmptyEnumerableWhenElasticReturnIsInvalid()
        { /** It's not possible mock response */ }

        [Fact]
        public void ShouldReturnAnswerDocumentsResponse()
        {/** It's not possible mock response */}
    }
}