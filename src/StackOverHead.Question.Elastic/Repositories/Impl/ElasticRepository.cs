using System;
using System.Linq;
using System.Threading.Tasks;
using Nest;
using StackOverHead.Question.Elastic.Models;

namespace StackOverHead.Question.Elastic.Repositories.Impl
{
    public class ElasticRepository : IElasticRepository
    {
        private readonly IElasticClient _client;

        public ElasticRepository(IElasticClient client)
        {
            _client = client;
        }
        public async Task AddAnswerAsync(Guid questionId, AnswerModel answer)
        {
            var question = await GetQuestionById(questionId);
            question.Answers.Add(answer);
            await UpdateQuestionAsync(question);
        }

        public async Task AddAnswerCommentAsync(Guid questionId, Guid answerId, CommentModel comment)
        {
            var question = await GetQuestionById(questionId);
            var answer = question.Answers.FirstOrDefault(a => a.Id == answerId);
            answer.Comments.Add(comment);
            await UpdateQuestionAsync(question);
        }

        public async Task AddQuestionAsync(QuestionModel question)
        {
            await _client.IndexDocumentAsync<QuestionModel>(question);
        }

        public async Task AddQuestionCommentAsync(Guid questionId, CommentModel comment)
        {
            var question = await GetQuestionById(questionId);
            question.Comments.Add(comment);
            await UpdateQuestionAsync(question);
        }

        public async Task UpdateQuestionAsync(QuestionModel question)
        {
            await _client.IndexDocumentAsync<QuestionModel>(question);
        }

        private async Task<QuestionModel> GetQuestionById(Guid id)
        {
            var response = await _client.GetAsync<QuestionModel>(id);
            return response.Source;
        }
    }
}