using System;
using System.Threading.Tasks;

using StackOverHead.Question.Elastic.Models;

namespace StackOverHead.Question.Elastic.Repositories
{
    public interface IElasticRepository
    {
        Task AddQuestionAsync(QuestionModel question);

        Task UpdateQuestionAsync(QuestionModel question);
        Task AddQuestionCommentAsync(Guid questionId, CommentModel comment);
        Task AddAnswerAsync(Guid questionId, AnswerModel answer);
        Task AddAnswerCommentAsync(Guid questionId, Guid answerId, CommentModel comment);
    }
}