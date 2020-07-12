using System.Collections.Generic;
using System.Threading.Tasks;
using Nest;
using StackOverHead.Question.Elastic.Exceptions;
using StackOverHead.Question.Elastic.Models;

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

        public async Task UpdateAsync(Answer model)
        {
            var answer = await _client.GetAsync<Answer>(model.Id);
            if (answer is null)
                throw new DocumentNotFoundElkException(model.Id);
            await _client.IndexDocumentAsync<Answer>(model);
        }

        public async Task RemoveAsync(Answer model) =>
            await _client.DeleteAsync<Answer>(model);

        public async Task<IEnumerable<Answer>> SearchAsync(
            string question,
            string tags,
            int page,
            int pageSize)
        {
            var answers = await _client.SearchAsync<Answer>(s => s
                .Query(q =>
                {
                    q.Match(m => m
                        .Field(f => f.Content)
                        .Query(question));
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