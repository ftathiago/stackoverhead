using Nest;
using StackOverHead.Question.Elastic.Exceptions;
using StackOverHead.Question.Elastic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StackOverHead.Question.Elastic.Repositories.Impl
{
    public class Answers : IAnswerRepository
    {
        private readonly IElasticClient _client;

        public Answers(IElasticClient client)
        {
            _client = client;
        }

        public async Task AddAsync(Answer answer)
        {
            await _client.IndexDocumentAsync<Answer>(answer);
        }

        public async Task UpdateAsync(Answer answer)
        {
            var answerResponse = await _client.GetAsync<Answer>(answer.Id);
            if (!answerResponse.Found)
                throw new DocumentNotFoundElkException(answer.Id);
            await _client.IndexDocumentAsync<Answer>(answer);
        }

        public async Task RemoveAsync(Answer answer) =>
            await _client.DeleteAsync<Answer>(answer);

        public async Task<IEnumerable<Answer>> SearchAsync(
            string content,
            string tags,
            int page,
            int pageSize)
        {
            var answers = await _client.SearchAsync<Answer>(s => s
                .Query(q =>
                {
                    q.Match(m => m
                        .Field(f => f.Content)
                        .Query(content));
                    if (string.IsNullOrEmpty(tags))
                        return q;
                    return q && q.Match(m => m
                        .Field(f => f.Tags)
                        .Query(tags));
                })
            );
            if (!answers.IsValid)
            {
                return new Answer[0];
            }
            return answers.Documents;
        }
    }
}