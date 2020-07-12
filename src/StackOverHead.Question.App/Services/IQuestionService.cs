using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using StackOverHead.Question.App.Models;

namespace StackOverHead.Question.App.Services
{
    public interface IQuestionService
    {
        Task<QuestionResponse> GetById(Guid id);
        Task<Guid> Add(AskQuestion request);
        Task<Guid> RegisterQuestionComment(QuestionCommentRequest request);
        Task<Guid> RegisterAnswer(AnswerRequest request);
        Task<Guid> RegisterAnswerComment(AnswerCommentRequest request);
        Task<IEnumerable<SearchQuestionResponse>> Search(
            string question,
            string tags,
            int page,
            int pageSize);
    }
}