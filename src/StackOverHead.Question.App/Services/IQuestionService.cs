using System;
using System.Threading.Tasks;

using StackOverHead.Question.App.Models;

namespace StackOverHead.Question.App.Services
{
    public interface IQuestionService
    {
        Task<QuestionResponse> GetById(Guid id);
        Task<Guid> Add(AskQuestion question);
        Task<Guid> RegisterAnswer(AnswerRequest request);
        Task<Guid> RegisterAnswerComment(AnswerCommentRequest request);
    }
}